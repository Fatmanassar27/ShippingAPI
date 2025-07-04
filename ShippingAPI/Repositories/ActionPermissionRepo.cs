using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class ActionPermissionRepo : GenericRepo<ActionType>
    {
        public ActionPermissionRepo(ShippingContext db) : base(db)
        {
        }
    }
}
