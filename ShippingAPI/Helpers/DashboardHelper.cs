using ShippingAPI.DTOS.DashBoardDTOs;
using ShippingAPI.Models;

namespace ShippingAPI.Helpers
{
    public class DashboardHelper
    {
        public static DashboardStatsDto CalculateStats(IEnumerable<Order> orders)
        {
            return new DashboardStatsDto
            {
                NewOrders = orders.Count(o => o.Status == OrderStatus.New),
                PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                DeliveredToRepresentativeOrders = orders.Count(o => o.Status == OrderStatus.DeliveredToCourier),
                DeliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                UnreachableOrders = orders.Count(o => o.Status == OrderStatus.NotReachable),
                PostponedOrders = orders.Count(o => o.Status == OrderStatus.Postponed),
                PartiallyDeliveredOrders = orders.Count(o => o.Status == OrderStatus.PartiallyDelivered),
                CancelledByUserOrders = orders.Count(o => o.Status == OrderStatus.CancelledByRecipient),
                RejectedWithPaymentOrders = orders.Count(o => o.Status == OrderStatus.RejectedWithPayment),
                RejectedWithPartialPaymentOrders = orders.Count(o => o.Status == OrderStatus.RejectedWithPartialPayment),
                RejectedWithoutPaymentOrders = orders.Count(o => o.Status == OrderStatus.RejectedWithoutPayment),
                TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.OrderCost),
                TotalCost = orders.Sum(o => o.TotalCost)
            };


        }
    }
}
