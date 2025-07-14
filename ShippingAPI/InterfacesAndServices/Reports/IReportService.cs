using ShippingAPI.DTOS.Reports.OrderDelivery;

namespace ShippingAPI.InterfacesAndServices.Reports
{
    public interface IReportService
    {
        Task<List<OrderReportDto>> GetOrderReportAsync(OrderReportRequestDto request);
    }
}
