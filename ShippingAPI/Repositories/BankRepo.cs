using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class BankRepo : GenericRepo<Bank>
    {
        public BankRepo(ShippingContext db) : base(db)
        {
        }
    }
}
