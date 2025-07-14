namespace ShippingAPI.DTOS.courier
{
    public class OrderDisplayDTO
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
        public string MerchantName { get; set; }

        public string Governorate { get; set; }

        public string city { get; set; }

        public string branch { get; set; }

        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal OrderCost { get; set; }

    }
}
