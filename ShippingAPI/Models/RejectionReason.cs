using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.Models
{
    public class RejectionReason
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
