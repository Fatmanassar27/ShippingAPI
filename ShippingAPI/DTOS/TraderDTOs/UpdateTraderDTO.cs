namespace ShippingAPI.DTOS.TraderDTOs
{
    public class UpdateTraderDTO
    {
        public string StoreName { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int BranchId { get; set; }
        public decimal CustomPickupCost { get; set; }
        public decimal RejectedOrderShippingShare { get; set; }
    }
}
