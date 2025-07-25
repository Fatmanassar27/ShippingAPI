﻿using ShippingAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.OrderDTOs
{
    public class addOrderDTO
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string StreetAddress { get; set; }

        public DeliveryType DeliveryType { get; set; } = DeliveryType.AtBranch;
        public bool DeliverToVillage { get; set; } = false;
        public PaymentType PaymentType { get; set; } = PaymentType.Prepaid;

        public int ShippingTypeId { get; set; } 
        public double TotalWeight { get; set; }
        public decimal OrderCost { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? RejectionReasonId { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }

        public int? BranchId { get; set; }
        public string? TraderId { get; set; }
        public string? CourierId { get; set; }
    }
}
