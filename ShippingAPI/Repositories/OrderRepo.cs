using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class OrderRepo:GenericRepo<Order>
    {
        public OrderRepo(ShippingContext db) : base(db){ }

        public List<Order> getAllWithObj()
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.Branch).Include(o=>o.ShippingType).Include(o => o.CourierProfile)
                .ToList();
        }
        public Order getByIdWithObj(int id)
        {
            return db.Orders.Include(o=>o.City).Include(o=>o.Governorate).Include(o=>o.TraderProfile)
                .Include(o=>o.RejectionReason).Include(o => o.ShippingType).Include(o=>o.Branch).Include(o => o.CourierProfile)
                .FirstOrDefault(o=>o.Id == id);
        }

        public List<Order> getAllByTraderId(string traderId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o=>o.TraderId == traderId).ToList();
        }
        public List<Order> getAllByCourierId(string courierId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o => o.CourierId == courierId).ToList();
        }
        public List<Order> getAllByBranchId(int BranchId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o => o.BranchId == BranchId).ToList();
        }
        public List<Order> getAllByGovId(int govId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o => o.GovernorateId == govId).ToList();
        }
        public List<Order> getAllByCityId(int cityId)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o => o.CityId == cityId).ToList();
        }
        public List<Order> getAllByStatus(OrderStatus status)
        {
            return db.Orders.Include(o => o.City).Include(o => o.Governorate).Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason).Include(o => o.ShippingType).Include(o => o.Branch).Include(o => o.CourierProfile)
                .Where(o => o.Status == status).ToList();
        }

        public List<Order> GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
        {
            return db.Orders
                .Include(o => o.City)
                .Include(o => o.Governorate)
                .Include(o => o.TraderProfile)
                .Include(o => o.RejectionReason)
                .Include(o => o.ShippingType)
                .Include(o => o.Branch)
                .Include(o => o.CourierProfile)
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .ToList();
        }

        //public decimal CalculateTotalCost(Order order)
        //{
        //    decimal totalCost = order.OrderCost;

        //    if (order.DeliverToVillage)
        //    {
        //        var extra = db.ExtraVillagePrice.FirstOrDefault(p => p.IsActive);
        //        if (extra != null)
        //            totalCost += extra.Value;
        //    }

        //    var weight = db.Weights.FirstOrDefault();
        //    if (weight != null && order.TotalWeight > (double)weight.Value)
        //    {
        //        var extraWeight = order.TotalWeight - (double)weight.Value;
        //        totalCost += (decimal)extraWeight * weight.PricePerExtraKg;
        //    }

        //    return totalCost;
        //}

        //-------------------------------------------------

        public decimal CalculateTotalCost(Order order)
        {
            decimal totalCost = order.OrderCost;

            // هشوف لو توصيل لقرية 
            if (order.DeliverToVillage)
            {
                var extraVillage = db.ExtraVillagePrice.FirstOrDefault(p => p.IsActive);
                if (extraVillage != null)
                    totalCost += extraVillage.Value;
            }

            
            var city = db.Cities.FirstOrDefault(c => c.Id == order.CityId);
            var trader = db.TraderProfiles.FirstOrDefault(t => t.UserId == order.TraderId);

            // هشوف لو عنده سعر خاص ليه لو لا هضيف بتاع المدينة دي عادي خالص
            if (city != null)
            {
                decimal cityDeliveryCost = 0;

                if (trader != null)
                {
                    var customCityPrice = db.CustomPrices
                        .FirstOrDefault(p => p.TraderId == trader.UserId && p.CityId == city.Id);

                    cityDeliveryCost = customCityPrice?.Price ?? city.PricePerKg;
                }
                else
                {
                    cityDeliveryCost = city.PricePerKg;
                }

                totalCost += cityDeliveryCost;
            }

            //  لو الوزن ازيد من القياسي
            var weight = db.Weights.FirstOrDefault();
            if (weight != null && order.TotalWeight > (double)weight.Value)
            {
                var extraWeight = order.TotalWeight - (double)weight.Value;
                totalCost += (decimal)extraWeight * weight.PricePerExtraKg;
            }

            // لو هيروح يجيب الاوردر من عند التاجر وهشوف لو عنده سعر خاص بيه او اضيف بتاع المدينة 
            if (order.DeliveryType == DeliveryType.FromMerchant)
            {
                if (trader != null)
                {
                    if (trader.CustomPickupCost > 0)
                    {
                        totalCost += trader.CustomPickupCost;
                    }
                    else if (city != null)
                    {
                        totalCost += city.PickupCost;
                    }
                }
            }
            var shippingType = db.ShippingTypes.FirstOrDefault(s => s.Id == order.ShippingTypeId);
            if (shippingType != null)
            {
                totalCost += shippingType.AdditionalCost;
            }

            return totalCost;
        }
        public IQueryable<Order> GetQueryable()
        {
            return db.Orders.AsQueryable(); 
        }





    }
}
