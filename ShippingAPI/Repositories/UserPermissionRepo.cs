using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class UserPermissionRepo : GenericRepo<UserPermission>
    {
        public UserPermissionRepo(ShippingContext db) : base(db)
        { }
    }
}
