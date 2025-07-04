using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class ExtraVillagePriceRepo : GenericRepo<ExtraVillagePrice>
    {
        public ExtraVillagePriceRepo(ShippingContext db) : base(db)
        {
        }
    }
}
