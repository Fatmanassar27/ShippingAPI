using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.city_govern
{
    public class governrateidDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;



    }
}
