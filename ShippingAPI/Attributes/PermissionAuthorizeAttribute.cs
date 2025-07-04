using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.Interfaces.Permissions;

namespace ShippingAPI.Attributes
{
    public class PermissionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permissionName;
        private readonly int _actionTypeId;

        public PermissionAuthorizeAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var service = context.HttpContext.RequestServices.GetService<IPermissionService>();
            var userId = context.HttpContext.User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId) || service == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var hasPermission = await service.HasPermissionAsync(new CheckPermissionDto
            {
                UserId = userId,
                PermissionName = _permissionName
                // بدون ActionTypeId
            });

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
