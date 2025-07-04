using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Weight
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "Money")]
        public decimal PricePerExtraKg { get; set; }

    }
}
