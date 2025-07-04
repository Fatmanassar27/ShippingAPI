using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.WeightDTOs
{
    public class addWeightDTO
    {
        public decimal Value { get; set; }
        public decimal PricePerExtraKg { get; set; }
    }
}
