using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class EmployeeSafe
    {
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        public int SafeId { get; set; }
        public virtual Safe Safe { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}
