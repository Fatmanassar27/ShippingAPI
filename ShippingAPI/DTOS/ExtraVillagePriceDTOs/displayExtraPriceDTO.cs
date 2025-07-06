using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.ExtraVillagePriceDTOs
{
    public class displayExtraPriceDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public bool IsActive { get; set; }
    }
}
