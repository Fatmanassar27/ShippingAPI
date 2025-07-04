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
        public PaymentType PaymentType { get; set; }
        public double TotalWeight { get; set; }
        public decimal TotalCost { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


        public string RejectionReason { get; set; } = "";


        //محافظة العميل 
        public string GovernorateName { get; set; }
        public string CityName { get; set; }

        public string BranchName { get; set; }

        //تاجر 

        public string? TraderName { get; set; }
        public string? CourierName { get; set; }


    }
}
