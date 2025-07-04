using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class AdminGroup
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } // HR - Accounting - SuperAdmin

        public virtual ICollection<AdminProfile> Admins { get; set; }

        public virtual ICollection<AdminGroupPermission> Permissions { get; set; }
    }
}
