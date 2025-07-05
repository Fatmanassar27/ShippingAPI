using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class BranchRepo : GenericRepo<Branch>
    {
        public BranchRepo(ShippingContext db) : base(db)
        {
        }
        public Branch? getByName(string name)
        {
            return db.Branches.FirstOrDefault(b => b.Name.ToLower() == name.ToLower());
        }
    }
}
