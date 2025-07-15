using ShippingAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.OrderDTOs
{
    public class displayOrderDTO
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string StreetAddress { get; set; }

        public DeliveryType DeliveryType { get; set; }
        public bool DeliverToVillage { get; set; }
        public string ShippingTypeName { get; set; }
        public int ShippingTypeId { get; set; }

        public PaymentType PaymentType { get; set; }
        public double TotalWeight { get; set; }
        public decimal OrderCost { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


        public string RejectionReason { get; set; } = "";
        public int RejectionReasonId { get; set; }
        public string GovernorateName { get; set; }
        public int GovernorateId { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }

        public string BranchName { get; set; }
        public int BranchId { get; set; }

        public string? TraderName { get; set; }
        public string? TraderId { get; set; }
        public string? CourierName { get; set; }
        public string? CourierId { get; set; }


    }
}
