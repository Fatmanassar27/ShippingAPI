using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }

        [Column(TypeName = "Money")]
        public decimal PricePerKg { get; set; }
        //  تكلفة البيك أب
        [Column(TypeName = "Money")]
        public decimal PickupCost { get; set; } = 0;

        //  الحالة
        public bool IsActive { get; set; } = true;
        public virtual Governorate Governorate { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
        public virtual ICollection<TraderProfile> TraderProfiles { get; set; } = new List<TraderProfile>();
    }
}
