using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class CustomPrice
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        //تاجر 
        [ForeignKey("TraderProfile")]
        public string TraderId { get; set; }
        public virtual TraderProfile TraderProfile { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public bool IsActive { get; set; } = false;

    }
}
