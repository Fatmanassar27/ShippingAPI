namespace ShippingAPI.DTOS.TraderDTOs
{
    public class TraderProfileDTO
    {
        public string UserId { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public decimal CustomPickupCost { get; set; }
        public decimal RejectedOrderShippingShare { get; set; }
    }
}
