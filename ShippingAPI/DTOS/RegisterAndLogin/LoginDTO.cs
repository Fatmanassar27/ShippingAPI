using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.RegisterAndLogin
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
