using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class ShippingType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [Column(TypeName = "Money")]
        public decimal AdditionalCost { get; set; }

        public int EstimatedDays { get; set; }

        public bool isActive { get; set; }= false;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
