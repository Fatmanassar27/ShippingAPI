namespace ShippingAPI.DTOS.Reports.OrderDelivery
{
    public class OrderReportDto
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string TraderName { get; set; }
        public string CourierName { get; set; }
        public string BranchName { get; set; }
        public string GovernorrateName { get; set; }
        public string CityName { get; set; }
        public string StatusName { get; set; }
        public decimal OrderCost { get; set; }
        public decimal TotalCost { get; set; }
        public double TotalWeight { get; set; }
        public int SerialNumber { get; set; }
    }
}
