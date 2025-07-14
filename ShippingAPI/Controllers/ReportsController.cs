using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.Reports.OrderDelivery;
using ShippingAPI.InterfacesAndServices.Reports;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
