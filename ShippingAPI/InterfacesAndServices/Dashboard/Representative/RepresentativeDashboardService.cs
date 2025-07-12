using ShippingAPI.DTOS.DashBoardDTOs;
using ShippingAPI.Helpers;
using ShippingAPI.Interfaces.Dashboard;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.InterfacesAndServices.Dashboard.Representative
{
    public class RepresentativeDashboardService : IRepresentativeDashboardService
    {
        private readonly UnitOfWork unitOfWork;

        public RepresentativeDashboardService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DashboardStatsDto> GetDashboardStatsAsync(string userId)
        {
            var orders = await unitOfWork.OrderRepo.WhereAsync(o => o.CourierId == userId);
            return DashboardHelper.CalculateStats(orders);
        }
    }
}
