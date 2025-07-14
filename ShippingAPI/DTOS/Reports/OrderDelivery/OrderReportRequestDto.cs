using ShippingAPI.Models;

namespace ShippingAPI.DTOS.Reports.OrderDelivery
{
    public class OrderReportRequestDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? BranchId { get; set; }
        public int? GovernorateId { get; set; }
        public int? CityId { get; set; }
        public string? TraderId { get; set; }
        public string? CourierId { get; set; }
        public OrderStatus? Status { get; set; }
        public PaymentType? PaymentType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
