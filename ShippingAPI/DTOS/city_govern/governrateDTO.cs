using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.city_govern
{
    public class governrateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;



    }
}
