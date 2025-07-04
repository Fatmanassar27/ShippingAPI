using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class CourierBranch
    {
        [Key]
        public int Id { get; set; }

        public string CourierId { get; set; }
        [ForeignKey("CourierId")]
        public virtual CourierProfile Courier { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
    }
}
