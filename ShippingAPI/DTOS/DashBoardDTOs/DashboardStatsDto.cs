namespace ShippingAPI.DTOS.DashBoardDTOs
{
    public class DashboardStatsDto
    {
        public int NewOrders { get; set; } 
        public int PendingOrders { get; set; } 
        public int DeliveredOrders { get; set; } 
        public int DeliveredToRepresentativeOrders { get; set; } 
        public int UnreachableOrders { get; set; } 
        public int PostponedOrders { get; set; } 
        public int PartiallyDeliveredOrders { get; set; } 
        public int CancelledByUserOrders { get; set; } 
        public int RejectedWithPaymentOrders { get; set; } 
        public int RejectedWithPartialPaymentOrders { get; set; } 
        public int RejectedWithoutPaymentOrders { get; set; } 
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
    }
}
