using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.city_govern
{
    public class BranchIDdto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int CityId { get; set; } 
    }
}
