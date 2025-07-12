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
                NewOrders = orders.Count(o => o.Status == OrderStatus.Pending),
                PendingOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                DeliveredToRepresentativeOrders = orders.Count(o => o.Status == OrderStatus.DeliveredToCourier),
                DeliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                UnreachableOrders = orders.Count(o => o.RejectionReason?.Reason == "Unreachable"),
                PostponedOrders = orders.Count(o => o.RejectionReason?.Reason == "Postponed"),
                PartiallyDeliveredOrders = orders.Count(o => o.RejectionReason?.Reason == "Partially Delivered"),
                CancelledByUserOrders = orders.Count(o => o.RejectionReason?.Reason == "Cancelled by User"),
                RejectedWithPaymentOrders = orders.Count(o => o.RejectionReason?.Reason == "Rejected with Payment"),
                RejectedWithPartialPaymentOrders = orders.Count(o => o.RejectionReason?.Reason == "Rejected with Partial Payment"),
                RejectedWithoutPaymentOrders = orders.Count(o => o.RejectionReason?.Reason == "Rejected without Payment"),
                TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.OrderCost),
                TotalCost = orders.Sum(o => o.TotalCost)
            };


        }
    }
}
