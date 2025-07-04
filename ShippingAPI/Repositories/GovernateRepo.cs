using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class GovernateRepo : GenericRepo<Governorate>
    {
        public GovernateRepo(ShippingContext db) : base(db)
        {
        }
    }
}
