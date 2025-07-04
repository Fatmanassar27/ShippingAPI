using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class OrderItemRepo : GenericRepo<OrderItem>
    {
        public OrderItemRepo(ShippingContext db) : base(db){}

        public List<OrderItem> getAllOrderItemsWithOrder()
        {
            return db.OrderItems.Include(oi => oi.Order).ToList();
        }
        public OrderItem getOrderItemByIdWithOrder(int orderItemId)
        {
            return db.OrderItems.Include(oi => oi.Order).FirstOrDefault(x => x.OrderId == orderItemId);
        }
    }
}
