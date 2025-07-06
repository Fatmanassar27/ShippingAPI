using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.ExtraVillagePriceDTOs
{
    public class addExtraPriceDTO
    {
        [Column(TypeName = "Money")]
        public decimal Value { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
