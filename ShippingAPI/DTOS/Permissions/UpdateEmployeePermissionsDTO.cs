namespace ShippingAPI.DTOS.Permissions
{
    public class UpdateEmployeePermissionsDTO
    {
        public string UserId { get; set; }
        public List<int> PermissionIds { get; set; } = new();
    }
}
