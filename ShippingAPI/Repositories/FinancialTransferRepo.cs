using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class FinancialTransferRepo : GenericRepo<FinancialTransfer>
    {
        public FinancialTransferRepo(ShippingContext db) : base(db)
        {
        }
    }
}
