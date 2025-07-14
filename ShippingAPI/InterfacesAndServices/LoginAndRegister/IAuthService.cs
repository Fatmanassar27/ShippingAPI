using Microsoft.EntityFrameworkCore;
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
        Task<List<EmployeeWithPermissionsDTO>> GetAllEmployeesWithPermissionsAsync();
        Task<EmployeeWithPermissionsDTO?> GetEmployeeWithPermissionsByIdAsync(string userId);
        Task<bool> ToggleEmployeeStatusAsync(string userId, bool isActive);
        Task<bool> UpdateEmployeeAsync(UpdateEmployeeDTO dto);
        Task<bool> LogoutAsync(string userId);



    }
}
