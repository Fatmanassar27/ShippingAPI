using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class AdminGroupPermission
    {
        [Key]
        public int Id { get; set; }

        public int GroupId { get; set; }
        public virtual AdminGroup Group { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
