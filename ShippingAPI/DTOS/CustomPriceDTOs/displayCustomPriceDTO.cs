using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.CustomPriceDTOs
{
    public class displayCustomPriceDTO
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public String CityName { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

    }
}
