using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DeliveryType DeliveryType { get; set; } = DeliveryType.AtBranch;

        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        //محافظة العميل 
        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }

        //مدينة العميل 
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string StreetAddress { get; set; }

        public bool DeliverToVillage { get; set; } = false;

        [ForeignKey("ShippingType")]
        public int? ShippingTypeId { get; set; }
        public virtual ShippingType? ShippingType { get; set; }

        public PaymentType PaymentType { get; set; } = PaymentType.Prepaid;

        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        public double TotalWeight { get; set; }
        public decimal OrderCost { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //تاجر 
        [ForeignKey("TraderProfile")]
        public string? TraderId { get; set; }
        public virtual TraderProfile? TraderProfile { get; set; }
        //مندوب
        [ForeignKey("CourierProfile")]
        public string? CourierId { get; set; }
        public virtual CourierProfile? CourierProfile { get; set; }

        [ForeignKey("RejectionReason")]
        public int? RejectionReasonId { get; set; }
        public virtual RejectionReason RejectionReason { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalCost { get; set; }


    }
    public enum OrderStatus
    {
        New = 1,
        Pending = 2,
        DeliveredToCourier = 3,
        Delivered = 4,
        NotReachable = 5, //لا يمكن الوصول
        Postponed = 6, //تم التأجيل
        PartiallyDelivered = 7,
        CancelledByRecipient = 8, //تم إلغاء الطلب من قبل المستلم
        RejectedWithPayment = 9,
        RejectedWithPartialPayment = 10,
        RejectedWithoutPayment = 11
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