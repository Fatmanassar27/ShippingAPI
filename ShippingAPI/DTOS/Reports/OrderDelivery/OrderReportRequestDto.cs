using ShippingAPI.Models;

namespace ShippingAPI.DTOS.Reports.OrderDelivery
{
    public class OrderReportRequestDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? BranchName { get; set; }
        public string? GovernorateName { get; set; }
        public string? CityName { get; set; }
        public string? TraderName { get; set; }
        public string? CourierName { get; set; }

        public OrderStatus? Status { get; set; }
        public PaymentType? PaymentType { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
