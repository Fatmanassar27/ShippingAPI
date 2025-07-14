using ShippingAPI.DTOS.Employee;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;

namespace ShippingAPI.Interfaces.LoginAndRegister
{
    public interface IAuthService
    {
        Task<UserProfileDTO?> RegisterAsync(RegisterDTO model);
        Task<UserProfileDTO?> LoginAsync(LoginDTO model);
        Task<UserProfileDTO?> RegisterToEmployeeAsync(RegisterEmployeeDTO dto);
        Task<List<EmployeeWithPermissionsDTO>> GetAllEmployeesWithPermissionsAsync(string? search = null);
        Task<bool> ToggleEmployeeStatusAsync(string userId, bool isActive);

    }
}
