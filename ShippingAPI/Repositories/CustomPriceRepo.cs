﻿using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CustomPriceRepo : GenericRepo<CustomPrice>
    {
        public CustomPriceRepo(ShippingContext db) : base(db) {}

        public List<CustomPrice> getAllWithObjs()
        {
            return db.CustomPrices.Include(cp=>cp.TraderProfile.User).Include(cp=>cp.City).ToList();
        }
        public CustomPrice getByIdWithObjs(int id)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).FirstOrDefault(cp => cp.Id == id);
        }

        public List<CustomPrice> getByUserId(string userId)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.TraderProfile.User.Id == userId).ToList();
        }

        public List<CustomPrice> getByRegionId(int regionId)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.CityId == regionId).ToList();
        }
        public List<CustomPrice> getByUserName(string userName)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.TraderProfile.User.UserName == userName).ToList();
        }

        public List<CustomPrice> getByCityId(int cityId)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.City.Id == cityId).ToList();
        }
        public List<CustomPrice> getByCityName(string cityName)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.City.Name == cityName).ToList();
        }
        public List<CustomPrice> getActivePrices()
        {
            return db.CustomPrices.Where(cp => cp.IsActive).ToList();
        }

        public List<CustomPrice> getByTraderId(string traderId)
        {
            return db.CustomPrices.Include(cp => cp.TraderProfile.User).Include(cp => cp.City).Where(cp => cp.TraderProfile.User.Id == traderId).ToList();
        }

        public List<CustomPrice> getCustomPricesByPrice(decimal Price) {
            return db.CustomPrices.Where(cp => cp.Price == Price).ToList();
        }


        public void addRange(List<CustomPrice> entities)
        {
            db.CustomPrices.AddRange(entities);
        }

    }
}
