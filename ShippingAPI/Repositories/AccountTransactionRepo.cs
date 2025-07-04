using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class AccountTransactionRepo : GenericRepo<AccountTransaction>
    {
        public AccountTransactionRepo(ShippingContext db) : base(db)
        {
        }
    }
}
