using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class WeightRepo :GenericRepo<Weight>
    {
        public WeightRepo(ShippingContext db) : base(db)
        {
        }
   
    }
}
