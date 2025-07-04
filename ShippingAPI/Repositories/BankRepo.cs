using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class BankRepo : GenericRepo<Bank>
    {
        public BankRepo(ShippingContext db) : base(db)
        {
        }
        public List<Bank> GetBanksByName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<Bank>();
            }
            return db.Set<Bank>().AsNoTracking()
                 .Where(b => b.Name.Contains(name)) // Case-insensitive search
                 .ToList();
        }
    }
}
