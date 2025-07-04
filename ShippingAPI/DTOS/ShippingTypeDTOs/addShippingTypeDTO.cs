using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.ShippingTypeDTOs
{
    public class addShippingTypeDTO
    {
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [Column(TypeName = "Money")]
        public decimal AdditionalCost { get; set; }
        public int EstimatedDays { get; set; }
        public bool isActive { get; set; } = false;

        }
}
