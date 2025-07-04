using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CourierBranchRepo : GenericRepo<CourierBranch>
    {
        public CourierBranchRepo(ShippingContext db) : base(db)
        {
        }
    }
}
