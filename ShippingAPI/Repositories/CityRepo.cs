using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CityRepo : GenericRepo<City>
    {
        public CityRepo(ShippingContext db) : base(db){}

        public City getByName(string name)
        {
            return db.Cities.FirstOrDefault(c => c.Name.Contains(name));
        }
        public City? getByName(string name)
        {

            return db.Cities.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefault();


        }
    }
}
