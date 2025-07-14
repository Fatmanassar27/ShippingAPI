using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.RegisterAndLogin
{
    public class RegisterEmployeeDTO
    {
        [Required] public string FullName { get; set; } = string.Empty;
        [Required] public string UserName { get; set; } = string.Empty;
        [Required][EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [Required] public List<int> BranchIds { get; set; } = new();
        [Required] public List<int> SafeIds { get; set; } = new();
        public List<int>? PermissionActionIds { get; set; }
    }
}
