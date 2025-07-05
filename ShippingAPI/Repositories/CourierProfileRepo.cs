using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class CourierProfileRepo : GenericRepo<CourierProfile>
    {
        public CourierProfileRepo(ShippingContext db) : base(db)
        {
        }
        public CourierProfile? getcourierbyname(string name)
        {
            return db.CourierProfiles.FirstOrDefault(c => c.User.UserName.ToLower() == name.ToLower());
        }

        public CourierProfile? getbyemail(string email)
        {
            return db.CourierProfiles.FirstOrDefault(c => c.User.Email.ToLower() == email.ToLower());
        }
    }
}
