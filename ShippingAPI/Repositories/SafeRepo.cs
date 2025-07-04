using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class SafeRepo : GenericRepo<Safe>
    {
        public SafeRepo(ShippingContext db) : base(db)
        { }
        public List<Safe> GetsafeByName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<Safe>();
            }
            return db.Set<Safe>().AsNoTracking()
                 .Where(b => b.Name.Contains(name)) // Case-insensitive search
                 .ToList();
        }
    }
}
