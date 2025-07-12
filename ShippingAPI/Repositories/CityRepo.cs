using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CityRepo : GenericRepo<City>
    {
        public CityRepo(ShippingContext db) : base(db){}


  
        public City? getByName(string name)
        {

            return db.Cities.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefault();


        }

        public List<City> getByGovernorateId(int governorateId)
        {
            return db.Cities.Include(c=>c.Governorate).Where(c => c.GovernorateId == governorateId).ToList();
        }
    }
}
