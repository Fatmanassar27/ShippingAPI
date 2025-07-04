using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class ExtraVillagePrice
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal Value { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
