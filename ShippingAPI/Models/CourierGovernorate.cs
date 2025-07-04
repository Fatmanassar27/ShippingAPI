using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class CourierGovernorate
    {
        [Key]
        public int Id { get; set; }

        public string CourierId { get; set; }
        [ForeignKey("CourierId")]
        public virtual CourierProfile Courier { get; set; }

        public int GovernorateId { get; set; }
        [ForeignKey("GovernorateId")]
        public virtual Governorate Governorate { get; set; }
    }
}
