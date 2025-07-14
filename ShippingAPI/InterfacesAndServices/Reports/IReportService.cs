using ShippingAPI.DTOS.Reports;
using ShippingAPI.DTOS.Reports.OrderDelivery;

namespace ShippingAPI.InterfacesAndServices.Reports
{
    public interface IReportService
    {
        Task<List<OrderReportDto>> GetOrderReportAsync(OrderReportRequestDto request);
        Task<List<OrderStatusLogDto>> GetOrderStatusLogsAsync(int orderId);
    }
}
