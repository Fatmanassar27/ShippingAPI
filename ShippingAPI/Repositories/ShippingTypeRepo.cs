using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class ShippingTypeRepo : GenericRepo<ShippingType>
    {
        public ShippingTypeRepo(ShippingContext db) : base(db){ }

        public List<ShippingType> getByTypeName(string typeName)
        {
            return db.ShippingTypes.Where(st => st.TypeName.Contains(typeName)).ToList();
        }
    }
}
