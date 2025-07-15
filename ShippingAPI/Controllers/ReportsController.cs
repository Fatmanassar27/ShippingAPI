using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.Reports.OrderDelivery;
using ShippingAPI.InterfacesAndServices.Reports;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetOrderReport([FromQuery] OrderReportRequestDto request)
        {
            var result = await reportService.GetOrderReportAsync(request);
            return Ok(result);
        }
        [HttpGet("order-status-history/{orderId}")]
        public async Task<IActionResult> GetOrderStatusHistory(int orderId)
        {
            var logs = await reportService.GetOrderStatusLogsAsync(orderId);

            if (logs == null || logs.Count == 0)
            {
                return NotFound($"No status logs found for order ID {orderId}.");
            }

            return Ok(logs);
        }

    }
}
