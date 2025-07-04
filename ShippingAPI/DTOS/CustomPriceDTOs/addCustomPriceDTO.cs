using System.ComponentModel.DataAnnotations.Schema;
using ShippingAPI.Models;

namespace ShippingAPI.DTOS.CustomPriceDTOs
{
    public class addCustomPriceDTO
    {

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public int  CityId { get; set; }

        public bool IsActive { get; set; } = false;
    }
}
