using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CityRepo : GenericRepo<City>
    {
        public CityRepo(ShippingContext db) : base(db)
        {
        }
    }
}
