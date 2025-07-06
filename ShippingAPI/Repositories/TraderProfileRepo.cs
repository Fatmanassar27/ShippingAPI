using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class TraderProfileRepo : GenericRepo<TraderProfile>
    {
        public TraderProfileRepo(ShippingContext db) : base(db){}

        public List<TraderProfile> getAllWithUser()
        {
            return db.TraderProfiles.Include(t => t.User).ToList();
        }

        public TraderProfile? getByIdWithUser(string userId)
        {
            return db.TraderProfiles.Include(t => t.User)
                .FirstOrDefault(t => t.UserId == userId);
        }

        public TraderProfile? getByStoreName(string name)
        {
            return db.TraderProfiles.Include(t => t.User)
                .FirstOrDefault(t => t.StoreName == name);
        }

        public void delete(string id)
        {
            var entity = db.TraderProfiles.FirstOrDefault(t => t.UserId == id);
            if (entity != null)
            {
                db.TraderProfiles.Remove(entity);
            }
        }
    }
}
