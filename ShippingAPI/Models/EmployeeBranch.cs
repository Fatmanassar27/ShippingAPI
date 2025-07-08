using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class EmployeeBranch
    {
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
    }
}
