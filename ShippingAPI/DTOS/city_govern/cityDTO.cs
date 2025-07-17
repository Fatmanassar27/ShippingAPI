using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.city_govern
{
    public class cityDTO
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int GovernorateId { get; set; }
        [Column(TypeName = "Money")]
        public decimal PricePerKg { get; set; }
        [Column(TypeName = "Money")]
        public decimal PickupCost { get; set; } = 0;
        public bool IsActive { get; set; } = true;

    }
}
