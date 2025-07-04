namespace ShippingAPI.Models
{
    public class UserPermission
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int PermissionActionId { get; set; }
        public virtual PermissionAction PermissionAction { get; set; } = null!;
    }
}
