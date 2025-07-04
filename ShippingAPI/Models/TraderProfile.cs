using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.Models
{
    public class TraderProfile
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string StoreName { get; set; }

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }


        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public decimal CustomPickupCost { get; set; }

        [Range(0, 100)]
        public decimal RejectedOrderShippingShare { get; set; }
    }
}
