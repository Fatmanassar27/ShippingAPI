using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public virtual ICollection<PermissionAction> PermissionActions { get; set; } = new List<PermissionAction>();
    }
}
