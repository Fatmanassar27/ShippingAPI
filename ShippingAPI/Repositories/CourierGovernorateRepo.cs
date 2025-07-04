using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CourierGovernorateRepo : GenericRepo<CourierGovernorate>
    {
        public CourierGovernorateRepo(ShippingContext db) : base(db)
        {
        }
    }
}
