using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class EmployeeSafeRepo : GenericRepo<EmployeeSafe>
    {
        public EmployeeSafeRepo(ShippingContext db) : base(db)
        {
        }
    }
}
