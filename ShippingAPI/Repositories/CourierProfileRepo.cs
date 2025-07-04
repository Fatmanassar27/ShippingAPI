using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CourierProfileRepo : GenericRepo<CourierProfile>
    {
        public CourierProfileRepo(ShippingContext db) : base(db)
        {
        }
    }
}
