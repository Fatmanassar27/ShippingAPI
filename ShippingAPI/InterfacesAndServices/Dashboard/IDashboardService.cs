using ShippingAPI.DTOS.DashBoardDTOs;

namespace ShippingAPI.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync(string userId);
    }
}
