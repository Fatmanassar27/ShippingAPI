using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.InterfacesAndServices.Dashboard.Representative;
using System.Security.Claims;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Courier")]

    public class RepresentativeDashboardController : ControllerBase
    {
        private readonly IRepresentativeDashboardService dashboardService;

        public RepresentativeDashboardController(IRepresentativeDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stats = await dashboardService.GetDashboardStatsAsync(userId);
            return Ok(stats);
        }
    }
}
