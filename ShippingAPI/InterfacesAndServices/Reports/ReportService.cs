using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var query =  unitOfWork.OrderRepo.GetQueryable();

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

            if (!string.IsNullOrEmpty(request.TraderId))
                query = query.Where(o => o.TraderId == request.TraderId);

            if (!string.IsNullOrEmpty(request.CourierId))
                query = query.Where(o => o.CourierId == request.CourierId);

            if (request.BranchId.HasValue)
                query = query.Where(o => o.BranchId == request.BranchId);

            if (request.CityId.HasValue)
                query = query.Where(o => o.CityId == request.CityId);

            if (request.GovernorateId.HasValue)
                query = query.Where(o => o.GovernorateId == request.GovernorateId);

            return query.OrderByDescending(o => o.CreatedAt);
        }
    }
}
