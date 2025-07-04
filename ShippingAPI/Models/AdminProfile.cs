using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class AdminProfile
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string JobTitle { get; set; } // مثال: HR, Finance, SuperAdmin

        public int AdminGroupId { get; set; }

        [ForeignKey("AdminGroupId")]
        public virtual AdminGroup AdminGroup { get; set; }
    }
}
