using ShippingAPI.DTOS.Permissions;

namespace ShippingAPI.Interfaces.Permissions
{
    public interface IPermissionService
    {
        Task<int> CreatePermissionAsync(PermissionDto dto);
        Task<int> CreateActionTypeAsync(ActionTypeDto dto);
        Task<int> CreatePermissionActionAsync(PermissionActionDto dto);
        Task<bool> AssignPermissionToUserAsync(AssignUserPermissionDto dto);
        Task<bool> HasPermissionAsync(CheckPermissionDto dto);

        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto?> GetPermissionByIdAsync(int id);

        Task<List<ActionTypeDto>> GetAllActionsAsync();
        Task<ActionTypeDto?> GetActionByIdAsync(int id);

        Task<bool> UpdatePermissionActionsAsync(UpdatePermissionActionsDto dto);
        Task<bool> RemovePermissionActionsAsync(RemovePermissionActionsDto dto);

        Task<PermissionWithActionsDto?> GetActionsByPermissionIdAsync(int permissionId);

    }
}
