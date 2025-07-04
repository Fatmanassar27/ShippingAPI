using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.WeightDTOs
{
    public class displayWeightDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal PricePerExtraKg { get; set; }
    }
}
