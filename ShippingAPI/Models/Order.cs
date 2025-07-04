using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string StreetAddress { get; set; }

        public DeliveryType DeliveryType { get; set; } = DeliveryType.AtBranch;
        public bool DeliverToVillage { get; set; } = false;
        public PaymentType PaymentType { get; set; } = PaymentType.Prepaid;
        public double TotalWeight { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("RejectionReason")]
        public int? RejectionReasonId { get; set; }
        public virtual RejectionReason RejectionReason { get; set; }

        //محافظة العميل 
        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }

        //مدينة العميل 
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();


        //تاجر 
        [ForeignKey("TraderProfile")]
        public string? TraderId { get; set; }
        public virtual TraderProfile? TraderProfile { get; set; }
        //مندوب
        [ForeignKey("CourierProfile")]
        public string? CourierId { get; set; }
        public virtual TraderProfile? CourierProfile { get; set; }

    }
    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        InTransit = 3,
        Delivered = 4,
        Rejected = 5,
        Cancelled = 6
    }

    public enum PaymentType
    {
        CollectOnDelivery = 1,   // واجبة التحصيل
        Prepaid = 2,              // دفع مقدم
        ExchangePackage = 3      // طرد مقابل طرد
    }
    public enum DeliveryType
    {
        AtBranch = 1,            // التسليم في الفرع
        FromMerchant = 2         // تسليم من التاجر
    }
}