namespace ShippingAPI.DTOS.Reports.OrderDelivery
{
    public class OrderReportResponseDto
    {
        public List<OrderReportDto> Orders { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public decimal TotalOrderCost { get; set; }
        public decimal TotalCost { get; set; }
        public double TotalWeight { get; set; }
    }
}
