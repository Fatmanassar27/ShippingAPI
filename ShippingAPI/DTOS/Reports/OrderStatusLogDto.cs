namespace ShippingAPI.DTOS.Reports
{
    public class OrderStatusLogDto
    {
        public int OrderId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public string Notes { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
