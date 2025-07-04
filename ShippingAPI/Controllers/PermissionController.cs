using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Attributes;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.Interfaces.Permissions;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpPost("create-permission")]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDto dto) =>
         Ok(new { PermissionId = await _permissionService.CreatePermissionAsync(dto) });

        [HttpPost("create-action-type")]
        public async Task<IActionResult> CreateActionType([FromBody] ActionTypeDto dto) =>
            Ok(new { ActionTypeId = await _permissionService.CreateActionTypeAsync(dto) });

        [HttpPost("create-permission-action")]
        public async Task<IActionResult> CreatePermissionAction([FromBody] PermissionActionDto dto) =>
            Ok(new { PermissionId = await _permissionService.CreatePermissionActionAsync(dto) });

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermissionToUser([FromBody] AssignUserPermissionDto dto)
        {
            var result = await _permissionService.AssignPermissionToUserAsync(dto);
            return result ? Ok("Permission assigned.") : BadRequest("Failed.");
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckPermission([FromBody] CheckPermissionDto dto)
        {
            var result = await _permissionService.HasPermissionAsync(dto);
            return Ok(new { HasPermission = result });
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissions() =>
            Ok(await _permissionService.GetAllPermissionsAsync());

        [HttpGet("permissions/{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            return permission is null ? NotFound() : Ok(permission);
        }

        [HttpGet("actions")]
        public async Task<IActionResult> GetAllActions() =>
            Ok(await _permissionService.GetAllActionsAsync());

        [HttpGet("actions/{id}")]
        public async Task<IActionResult> GetActionById(int id)
        {
            var action = await _permissionService.GetActionByIdAsync(id);
            return action is null ? NotFound() : Ok(action);
        }
        [HttpPut("update-permission-actions")]
        public async Task<IActionResult> UpdatePermissionActions([FromBody] UpdatePermissionActionsDto dto)
        {
            var result = await _permissionService.UpdatePermissionActionsAsync(dto);
            return result ? Ok("Actions updated for permission.") : NotFound("Permission not found.");
        }
        [HttpDelete("remove-permission-actions")]
        public async Task<IActionResult> RemovePermissionActions([FromBody] RemovePermissionActionsDto dto)
        {
            var result = await _permissionService.RemovePermissionActionsAsync(dto);
            return result ? Ok(" Selected actions removed from permission.") : NotFound(" No matching actions found.");
        }
        [HttpGet("{permissionId}/actions")]
        public async Task<IActionResult> GetActionsForPermission(int permissionId)
        {
            var result = await _permissionService.GetActionsByPermissionIdAsync(permissionId);
            return result is null ? NotFound("Permission not found") : Ok(result);
        }

        //[HttpGet("secure")]
        //[PermissionAuthorize("ManageOrders")]
        //public IActionResult TestSecure()
        //{
        //    return Ok("✔ Authorized to manage orders.");
        //}

    }
}
