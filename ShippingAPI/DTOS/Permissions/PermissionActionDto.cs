using System.Text.Json.Serialization;

namespace ShippingAPI.DTOS.Permissions
{
    public class PermissionActionDto
    {
        [JsonPropertyName("permissionId")]
        public int PermissionId { get; set; }
        [JsonPropertyName("actionTypeIds")]
        public List<int> ActionTypeIds { get; set; } = new();
    }
}
