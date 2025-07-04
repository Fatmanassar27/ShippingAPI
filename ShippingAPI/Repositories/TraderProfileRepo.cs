using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class TraderProfileRepo : GenericRepo<TraderProfile>
    {
        public TraderProfileRepo(ShippingContext db) : base(db)
        {
        }
    }
}
