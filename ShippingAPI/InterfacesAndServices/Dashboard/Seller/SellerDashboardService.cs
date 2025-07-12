using ShippingAPI.DTOS.DashBoardDTOs;
using ShippingAPI.Helpers;
using ShippingAPI.Interfaces.Dashboard;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.InterfacesAndServices.Dashboard.Seller
{
    public class SellerDashboardService : ISellerDashboardService
    {
        private readonly UnitOfWork unitOfWork;

        public SellerDashboardService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DashboardStatsDto> GetDashboardStatsAsync(string userId)
        {
            var orders = await unitOfWork.OrderRepo.WhereAsync(o => o.TraderId == userId);
            return DashboardHelper.CalculateStats(orders);
        }
    }
}
