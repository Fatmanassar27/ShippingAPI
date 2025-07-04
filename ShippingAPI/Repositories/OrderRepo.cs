using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class OrderRepo:GenericRepo<Order>
    {
        public OrderRepo(ShippingContext db) : base(db){ }

        public Order getByIdWithObj(int id)
        {
            return db.Orders.Include(o=>o.City).Include(o=>o.Governorate).Include(o=>o.TraderProfile)
                .Include(o=>o.RejectionReason).Include(o=>o.Branch).FirstOrDefault(o=>o.Id == id);
        }

        public List<Order> getAllByTraderId(string id)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.Branch).Where(o=>o.TraderId == id).ToList();
        }
        public List<Order> getAllByBranchId(int BranchId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.Branch).Where(o => o.BranchId == BranchId).ToList();
        }
        public List<Order> getAllByStatus(OrderStatus status)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.Branch).Where(o => o.Status == status).ToList();
        }

    }
}
