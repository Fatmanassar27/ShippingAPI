namespace ShippingAPI.DTOS.Permissions
{
    public class AssignUserPermissionDto
    {
        public string UserId { get; set; } = string.Empty;
        public int PermissionId { get; set; }
    }
}
