using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.Models
{
    public class CourierProfile
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        [Range(0, 1000)]
        public decimal OrderShare { get; set; }

        // علاقات Many-to-Many
        public virtual ICollection<CourierGovernorate> CourierGovernorates { get; set; }
        public virtual ICollection<CourierBranch> CourierBranches { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }
}
