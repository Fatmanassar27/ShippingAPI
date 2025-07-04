namespace ShippingAPI.DTOS.Permissions
{
    public class CheckPermissionDto
    {
        public string UserId { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
        public int ActionTypeId { get; set; }
    }
}
