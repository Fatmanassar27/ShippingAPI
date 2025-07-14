using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.Employee
{
    public class UpdateEmployeeDTO
    {
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "At least one branch is required")]
        public List<int> BranchIds { get; set; } = new();

        [Required(ErrorMessage = "At least one safe is required")]
        public List<int> SafeIds { get; set; } = new();

        public List<int>? PermissionActionIds { get; set; }
    }
}
