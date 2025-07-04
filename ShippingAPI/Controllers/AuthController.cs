using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.Interfaces.LoginAndRegister;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .FirstOrDefault();
                return BadRequest(new { message = error ?? "Invalid input" });
            }

            var profile = await authService.RegisterAsync(model);
            if (profile == null)
                return BadRequest(new { message = "Registration failed" });

            return Ok(profile);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid username or password format" });
            }

            var profile = await authService.LoginAsync(model);
            if (profile == null)
                return Unauthorized(new { message = "Username or password is incorrect, or account is inactive" });

            return Ok(profile);
        }
    }
}
