namespace ShippingAPI.DTOS.courier
{
    public class RejectedOrderDisplayDTO
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
        public string MerchantName { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Branch { get; set; }
        public decimal OrderCost { get; set; }

        public string RejectionReason { get; set; } 
    }
}
