using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class GovernateRepo : GenericRepo<Governorate>
    {
        public GovernateRepo(ShippingContext db) : base(db)
        {
        }
        public City? getByName(string name)
        {

            return db.Cities.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefault();


        }
    }
}
