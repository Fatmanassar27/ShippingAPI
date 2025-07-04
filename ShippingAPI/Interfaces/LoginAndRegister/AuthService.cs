using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShippingAPI.Interfaces.LoginAndRegister
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public AuthService(UserManager<ApplicationUser> userManager,
                       RoleManager<IdentityRole> roleManager,
                       IMapper mapper,
                       IConfiguration config)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.config = config;
        }

        public async Task<UserProfileDTO?> LoginAsync(LoginDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null || !user.IsActive)
                return null;

            var result = await userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return null;
            string token;
            if (!string.IsNullOrEmpty(user.CurrentToken) &&
                user.TokenExpiration.HasValue &&
                user.TokenExpiration > DateTime.UtcNow)
            {
                token = user.CurrentToken;
            }
            else
            {
                token = GenerateToken(user);
                user.CurrentToken = token;
                user.TokenExpiration = DateTime.UtcNow.AddDays(7);
                await userManager.UpdateAsync(user);
            }

            var roles = await userManager.GetRolesAsync(user);

            return new UserProfileDTO
            {

                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                Role = roles.FirstOrDefault(),
                Token = token,
                TokenExpiration = user.TokenExpiration
            };
        }

        public async Task<UserProfileDTO?> RegisterAsync(RegisterDTO model)
        {
            var user = mapper.Map<ApplicationUser>(model);
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return null;
            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                var roleExists = await roleManager.RoleExistsAsync(model.Role);
                if (roleExists)
                    await userManager.AddToRoleAsync(user, model.Role);
            }
            var token = GenerateToken(user);
            user.CurrentToken = token;
            user.TokenExpiration = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            var roles = await userManager.GetRolesAsync(user);
            var claims = await userManager.GetClaimsAsync(user);
            var positionClaim = claims.FirstOrDefault(c => c.Type == "Position");

            return new UserProfileDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                Role = roles.FirstOrDefault(),
                Token = token,
                TokenExpiration = user.TokenExpiration
            };
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
             new Claim("UserId", user.Id)
        };

            var roles = userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var userClaims = userManager.GetClaimsAsync(user).Result;
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
