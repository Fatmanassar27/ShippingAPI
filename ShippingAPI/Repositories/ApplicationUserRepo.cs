using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class ApplicationUserRepo:GenericRepo<ApplicationUser>
    {
        public ApplicationUserRepo(ShippingContext db) : base(db)
        {
        }
    
    }
}
