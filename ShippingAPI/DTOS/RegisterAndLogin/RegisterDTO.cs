using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.Register
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}
