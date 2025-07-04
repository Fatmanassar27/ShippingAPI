using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class SafeRepo : GenericRepo<Safe>
    {
        public SafeRepo(ShippingContext db) : base(db)
        { }
    }
}
