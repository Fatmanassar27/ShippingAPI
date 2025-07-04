using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();
        public virtual ICollection<Safe> Saves { get; set; } = new List<Safe>();
        public virtual ICollection<CourierBranch> CourierBranches { get; set; }
        public virtual ICollection<TraderProfile> TraderProfiles { get; set; }

    }
}
