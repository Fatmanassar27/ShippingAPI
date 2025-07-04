using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class AdminProfileRepo : GenericRepo<AdminProfile>
    {
        public AdminProfileRepo(ShippingContext db) : base(db)
        {
        }
    }
}
