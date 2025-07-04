using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Governorate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();
        public virtual ICollection<TraderProfile> TraderProfiles { get; set; } = new List<TraderProfile>();

    }
}
