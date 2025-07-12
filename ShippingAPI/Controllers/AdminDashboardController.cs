using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Interfaces.Dashboard;
using ShippingAPI.InterfacesAndServices.Dashboard.Admin;
using System.Security.Claims;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService dashboardService;

        public AdminDashboardController(IAdminDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await dashboardService.GetDashboardStatsAsync(null); 
            return Ok(stats);
        }
    }
}
