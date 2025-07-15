using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.InterfacesAndServices.Dashboard.Seller;
using System.Security.Claims;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Trader")]
    public class SellerDashboardController : ControllerBase
    {
        private readonly ISellerDashboardService dashboardService;

        public SellerDashboardController(ISellerDashboardService dashboardService)
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
