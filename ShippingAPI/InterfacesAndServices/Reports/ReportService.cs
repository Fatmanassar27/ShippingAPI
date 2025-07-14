using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShippingAPI.DTOS.Reports;
using ShippingAPI.DTOS.Reports.OrderDelivery;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.InterfacesAndServices.Reports
{
    public class ReportService : IReportService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReportService(UnitOfWork unitOfWork ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<OrderReportDto>> GetOrderReportAsync(OrderReportRequestDto request)
        {
            var query = BuildQuery(request);

            var orders = await query.ToListAsync();

            var orderDtos = mapper.Map<List<OrderReportDto>>(orders);

            for (int i = 0; i < orderDtos.Count; i++)
                orderDtos[i].SerialNumber = i + 1;

            return orderDtos;
        }
        private IQueryable<Order> BuildQuery(OrderReportRequestDto request)
        {
            var query = unitOfWork.OrderRepo.GetQueryable();

            if (request.FromDate.HasValue)
                query = query.Where(o => o.CreatedAt >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(o => o.CreatedAt <= request.ToDate.Value.AddDays(1));

            if (!string.IsNullOrEmpty(request.SearchTerm))
                query = query.Where(o =>
                    o.CustomerName.Contains(request.SearchTerm) ||
                    o.Phone1.Contains(request.SearchTerm) ||
                    o.Id.ToString().Contains(request.SearchTerm));

            if (request.Status.HasValue)
                query = query.Where(o => o.Status == request.Status);

            if (request.PaymentType.HasValue)
                query = query.Where(o => o.PaymentType == request.PaymentType);

            if (!string.IsNullOrEmpty(request.TraderName))
                query = query.Where(o => o.TraderProfile.User.FullName.Contains(request.TraderName));

            if (!string.IsNullOrEmpty(request.CourierName))
                query = query.Where(o => o.CourierProfile.User.FullName.Contains(request.CourierName));

            if (!string.IsNullOrEmpty(request.BranchName))
                query = query.Where(o => o.Branch.Name.Contains(request.BranchName));

            if (!string.IsNullOrEmpty(request.CityName))
                query = query.Where(o => o.City.Name.Contains(request.CityName));

            if (!string.IsNullOrEmpty(request.GovernorateName))
                query = query.Where(o => o.Governorate.Name.Contains(request.GovernorateName));

            return query.OrderByDescending(o => o.CreatedAt);
        }
        public async Task<List<OrderStatusLogDto>> GetOrderStatusLogsAsync(int orderId)
        {
            var logs = await unitOfWork.context.OrderStatusHistories
     .Where(o => o.OrderId == orderId)
     .OrderByDescending(o => o.ChangedAt)
     .ToListAsync();

            return mapper.Map<List<OrderStatusLogDto>>(logs);
        }
    }
}
