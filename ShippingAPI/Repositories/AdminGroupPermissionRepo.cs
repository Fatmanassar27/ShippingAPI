using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class AdminGroupPermissionRepo:GenericRepo<AdminGroupPermission>
    {
        public AdminGroupPermissionRepo(ShippingContext db) : base(db)
        {
        }
    }
    
}
