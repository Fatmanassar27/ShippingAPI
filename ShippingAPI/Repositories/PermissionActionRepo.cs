using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class PermissionActionRepo : GenericRepo<PermissionAction>
    {
        public PermissionActionRepo(ShippingContext db) : base(db)
        { }
        public async Task<PermissionAction?> GetByPermissionAndTypeIdAsync(int permissionId, int actionTypeId)
        {
            return await db.PermissionActions
                .FirstOrDefaultAsync(a => a.PermissionId == permissionId && a.ActionTypeId == actionTypeId);
        }
    }
}
