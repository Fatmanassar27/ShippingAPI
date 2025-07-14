namespace ShippingAPI.DTOS.courier
{
    public class UpdateOrderStatusDTO
    {
        public int OrderId { get; set; }
        public int NewStatus { get; set; }
    }
}
