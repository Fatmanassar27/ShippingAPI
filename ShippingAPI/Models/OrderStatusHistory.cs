using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderStatus OldStatus { get; set; }
        public OrderStatus NewStatus { get; set; }
        public string ChangedByUserId { get; set; }
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ChangedByUserId")]
        public virtual ApplicationUser ChangedByUser { get; set; }
    }
}
