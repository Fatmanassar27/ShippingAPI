using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShippingAPI.DTOS.Employee;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;
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
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public AuthService(UserManager<ApplicationUser> userManager,
                       RoleManager<IdentityRole> roleManager,
                       IMapper mapper,
                       UnitOfWork unitOfWork,
                       IConfiguration config)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Registration failed: {errors}");
            }
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

        public async Task<List<string>> GetRolesAsync()
        {
            return await Task.FromResult(roleManager.Roles.Select(r => r.Name).ToList());
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
        public async Task<UserProfileDTO?> RegisterToEmployeeAsync(RegisterEmployeeDTO dto)
        {
            var user = mapper.Map<ApplicationUser>(dto);
            user.IsActive = true;

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Registration failed: {errors}");
            }

            const string role = "Employee";
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            await userManager.AddToRoleAsync(user, role);

            var employeeBranches = dto.BranchIds.Distinct()
                .Select(branchId => new EmployeeBranch
                {
                    UserId = user.Id,
                    BranchId = branchId
                });
            await unitOfWork.EmployeeBranchRepo.AddRangeAsync(employeeBranches);
            var employeeSafes = dto.SafeIds.Distinct()
                .Select((safeId, index) => new EmployeeSafe
                {
                    UserId = user.Id,
                    SafeId = safeId,
                    IsDefault = index == 0
                });
            await unitOfWork.EmployeeSafeRepo.AddRangeAsync(employeeSafes);
            if (dto.PermissionActionIds?.Any() == true)
            {
                var permissions = dto.PermissionActionIds.Distinct()
                    .Select(paId => new UserPermission
                    {
                        UserId = user.Id,
                        PermissionActionId = paId
                    });
                await unitOfWork.UserPermissionRepo.AddRangeAsync(permissions);
            }

            await unitOfWork.SaveAsync();

            var token = GenerateToken(user);
            user.CurrentToken = token;
            user.TokenExpiration = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            return new UserProfileDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                Role = role,
                Token = token,
                TokenExpiration = user.TokenExpiration
            };
        }
        public async Task<List<EmployeeWithPermissionsDTO>> GetAllEmployeesWithPermissionsAsync()
        {

            var employeeRole = await roleManager.FindByNameAsync("Employee");
            if (employeeRole == null) return new List<EmployeeWithPermissionsDTO>();
            var employeeIds = await unitOfWork.context.UserRoles
                .Where(ur => ur.RoleId == employeeRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();
            var usersWithPermissions = await (
                from u in unitOfWork.context.Users
                where employeeIds.Contains(u.Id)
                join up in unitOfWork.context.UserPermissions on u.Id equals up.UserId into userPerms
                from up in userPerms.DefaultIfEmpty()
                join pa in unitOfWork.context.PermissionActions on up.PermissionActionId equals pa.Id into permActions
                from pa in permActions.DefaultIfEmpty()
                join p in unitOfWork.context.Permissions on pa.PermissionId equals p.Id into perms
                from p in perms.DefaultIfEmpty()
                join at in unitOfWork.context.ActionTypes on pa.ActionTypeId equals at.Id into actions
                from at in actions.DefaultIfEmpty()
                group new { u, p, at } by new
                {
                    u.Id,
                    u.UserName,
                    u.FullName,
                    u.Email,
                    u.Address,
                    u.IsActive
                }
                into g
                select new EmployeeWithPermissionsDTO
                {
                    UserId = g.Key.Id,
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Email = g.Key.Email,
                    Address = g.Key.Address,
                    IsActive = g.Key.IsActive,
                    Permissions = g
                        .Where(x => x.p != null && x.at != null)
                        .Select(x => $"{x.p.Name} - {x.at.Name}")
                        .Distinct()
                        .ToList()
                }
            ).ToListAsync();

            return usersWithPermissions;
        }

        public async Task<bool> ToggleEmployeeStatusAsync(string userId, bool isActive)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.IsActive = isActive;
            await userManager.UpdateAsync(user);
            return true;
        }
        public async Task<EmployeeWithPermissionsDTO?> GetEmployeeWithPermissionsByIdAsync(string userId)
        {
            var employeeRole = await roleManager.FindByNameAsync("Employee");
            if (employeeRole == null)
                return null;

            var isEmployee = await unitOfWork.context.UserRoles
                .AnyAsync(ur => ur.RoleId == employeeRole.Id && ur.UserId == userId);

            if (!isEmployee)
                return null;

            var result = await (
                from u in unitOfWork.context.Users
                where u.Id == userId
                join up in unitOfWork.context.UserPermissions on u.Id equals up.UserId into userPerms
                from up in userPerms.DefaultIfEmpty()
                join pa in unitOfWork.context.PermissionActions on up.PermissionActionId equals pa.Id into permActions
                from pa in permActions.DefaultIfEmpty()
                join p in unitOfWork.context.Permissions on pa.PermissionId equals p.Id into perms
                from p in perms.DefaultIfEmpty()
                join at in unitOfWork.context.ActionTypes on pa.ActionTypeId equals at.Id into actions
                from at in actions.DefaultIfEmpty()
                group new { u, p, at } by new
                {
                    u.Id,
                    u.UserName,
                    u.FullName,
                    u.Email,
                    u.Address,
                    u.IsActive
                }
                into g
                select new EmployeeWithPermissionsDTO
                {
                    UserId = g.Key.Id,
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Email = g.Key.Email,
                    Address = g.Key.Address,
                    IsActive = g.Key.IsActive,
                    Permissions = g
                        .Where(x => x.p != null && x.at != null)
                        .Select(x => $"{x.p.Name} - {x.at.Name}")
                        .Distinct()
                        .ToList()
                }
            ).FirstOrDefaultAsync();

            return result;
        }
       
    }
}
