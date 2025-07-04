namespace ShippingAPI.DTOS.Permissions
{
    public class UpdatePermissionActionsDto
    {
        public int PermissionId { get; set; }
        public List<int> ActionTypeIds { get; set; } = new();
    }
}
