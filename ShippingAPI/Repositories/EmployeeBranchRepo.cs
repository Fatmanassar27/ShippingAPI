using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class EmployeeBranchRepo : GenericRepo<EmployeeBranch>
    {
        public EmployeeBranchRepo(ShippingContext db) : base(db)
        {
        }
    }
}
