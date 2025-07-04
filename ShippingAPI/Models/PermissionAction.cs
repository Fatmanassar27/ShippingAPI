using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class PermissionAction
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; } = null!;

        public int ActionTypeId { get; set; }
        public virtual ActionType ActionType { get; set; } = null!;

        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
