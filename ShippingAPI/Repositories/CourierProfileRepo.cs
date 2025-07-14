using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CourierProfileRepo : GenericRepo<CourierProfile>
    {
        public CourierProfileRepo(ShippingContext db) : base(db)
        {
        }
        public CourierProfile? getcourierbyname(string name)
        {
            return db.CourierProfiles.Include(c=>c.User).FirstOrDefault(c => c.User.UserName.ToLower() == name.ToLower());
        }

        public CourierProfile? getbyemail(string email)
        {
            return db.CourierProfiles.Include(c => c.User).FirstOrDefault(c => c.User.Email.ToLower() == email.ToLower());
        }

        public List<CourierProfile> getAllWithUser()
        {
            return db.CourierProfiles.Include(t => t.User).ToList();
        }
        public bool hasRelatedBranches(string courierId)
        {
            return db.CourierBranches.Any(cb => cb.CourierId == courierId);
        }

        public bool hasRelatedGovernorates(string courierId)
        {
            return db.CourierGovernorates.Any(cg => cg.CourierId == courierId);
        }


        public CourierProfile? getByIdWithUser(string userId)
        {
            return db.CourierProfiles.Include(t => t.User)
                .FirstOrDefault(t => t.UserId == userId);
        }


        public List<Order> GetOrdersByCourierId(string courierId)
        {
            return db.Orders
                .Where(o => o.CourierId == courierId)
                .Include(o => o.Governorate)
                .Include(o => o.City)
                .Include(o => o.Branch)
                .Include(o => o.RejectionReason)
                .ToList();
        }





        public List<Order> GetRejectedOrdersByCourierId(string courierId)
        {
            return db.Orders
                .Where(o =>
                    o.CourierId == courierId &&
                    (o.Status == OrderStatus.RejectedWithPayment ||
                     o.Status == OrderStatus.RejectedWithPartialPayment ||
                     o.Status == OrderStatus.RejectedWithoutPayment))
                .Include(o => o.RejectionReason)
                .Include(o => o.City)
                .Include(o => o.Governorate)
                .Include(o => o.Branch)
                .ToList();
        }


        public List<RejectionReason> GetRejectionReasons()
        {
            return db.RejectionReasons.ToList();
        }

        public RejectionReason? GetRejectionReasonById(int id)
        {
            return db.RejectionReasons.FirstOrDefault(r => r.Id == id);
        }


    }
}
