using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.city_govern
{
    public class cityidDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string GoverrateName { get; set; }
        [Required]
        public int GovernorateId { get; set; }
        [Column(TypeName = "Money")]
        public decimal PricePerKg { get; set; }
        [Column(TypeName = "Money")]
        public decimal PickupCost { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
