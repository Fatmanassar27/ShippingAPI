using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class PermissionRepo:GenericRepo<Permission>
    {
        public PermissionRepo(ShippingContext db) : base(db)
        { }
    }
}
