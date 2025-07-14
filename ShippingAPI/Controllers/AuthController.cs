using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .FirstOrDefault();
                return BadRequest(new { message = error ?? "Invalid input" });
            }

            try
            {
                var profile = await authService.RegisterAsync(model);
                return Ok(profile);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
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
        [HttpPost("register-employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .FirstOrDefault();
                return BadRequest(new { message = error ?? "Invalid input" });
            }
            try
            {
                var profile = await authService.RegisterToEmployeeAsync(dto);
                return Ok(profile);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesWithPermissions()
        {
            var employees = await authService.GetAllEmployeesWithPermissionsAsync();
            return Ok(employees);
        }
        [HttpPut("toggle-status/{userId}")]
        public async Task<IActionResult> ToggleEmployeeStatus(string userId, [FromQuery] bool isActive)
        {
            var result = await authService.ToggleEmployeeStatusAsync(userId, isActive);
            if (!result)
                return NotFound($"User with ID {userId} not found");

            return Ok(new { message = $"Employee status updated to {(isActive ? "Active" : "Inactive")}" });
        }
        [HttpGet("employee/{userId}/permissions")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeePermissions(string userId)
        {
            try
            {
                var permissions = await authService.GetEmployeeWithPermissionsByIdAsync(userId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving permissions", details = ex.Message });
            }
        }

    }
}
