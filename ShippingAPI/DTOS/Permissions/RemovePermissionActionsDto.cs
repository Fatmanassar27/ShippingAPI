namespace ShippingAPI.DTOS.Permissions
{
    public class RemovePermissionActionsDto
    {
        public int PermissionId { get; set; }
        public List<int> ActionTypeIds { get; set; } = new();
    }
}
