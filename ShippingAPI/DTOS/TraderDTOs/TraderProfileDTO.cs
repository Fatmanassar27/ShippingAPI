namespace ShippingAPI.DTOS.TraderDTOs
{
    public class TraderProfileDTO
    {
        //public string UserId { get; set; }
        //public string StoreName { get; set; }
        //public string Email { get; set; }
        //public string FullName { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        //public string BranchName { get; set; }
        //public bool IsActive { get; set; }
        //public decimal CustomPickupCost { get; set; }
        //public decimal RejectedOrderShippingShare { get; set; }
        public string UserId { get; set; }
        public string StoreName { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }

        public decimal CustomPickupCost { get; set; }
        public decimal RejectedOrderShippingShare { get; set; }
    }
}
