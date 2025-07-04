using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class BranchRepo : GenericRepo<Branch>
    {
        public BranchRepo(ShippingContext db) : base(db)
        {
        }
    }
}
