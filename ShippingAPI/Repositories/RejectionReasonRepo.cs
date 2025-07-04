using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class RejectionReasonRepo : GenericRepo<RejectionReason>
    {
        public RejectionReasonRepo(ShippingContext db) : base(db)
        {
        }
    }
}
