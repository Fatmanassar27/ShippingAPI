using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.CustomPriceDTOs
{
    public class displayCustomPriceDTO
    {
        public int Id { get; set; }
        public string TraderId { get; set; }
        public String TraderName { get; set; }
        public int CityId { get; set; }
        public String CityName { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

    }
}
