using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;

namespace ShippingAPI.Interfaces.LoginAndRegister
{
    public interface IAuthService
    {
        Task<UserProfileDTO?> RegisterAsync(RegisterDTO model);
        Task<UserProfileDTO?> LoginAsync(LoginDTO model);
    }
}
