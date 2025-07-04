using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class AdminGroupRepo : GenericRepo<AdminGroup>
    {
        public AdminGroupRepo(ShippingContext db) : base(db)
        { }
    }
}

