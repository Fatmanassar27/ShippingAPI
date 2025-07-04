namespace ShippingAPI.DTOS.Permissions
{
    public class PermissionWithActionsDto
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public List<ActionTypeDto> Actions { get; set; } = new();
    }
}
