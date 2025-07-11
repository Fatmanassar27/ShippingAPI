using ShippingAPI.DTOS.DashBoardDTOs;
using ShippingAPI.Helpers;
using ShippingAPI.Interfaces.Dashboard;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.InterfacesAndServices.Dashboard.Admin
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly UnitOfWork unitOfWork;

        public AdminDashboardService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DashboardStatsDto> GetDashboardStatsAsync(string userId)
        {
            var orders = await unitOfWork.OrderRepo.GetAllAsync();
            return DashboardHelper.CalculateStats(orders);
        }
    }
}
