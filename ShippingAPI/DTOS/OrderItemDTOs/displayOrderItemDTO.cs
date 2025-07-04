using ShippingAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.OrderItemDTOs
{
    public class displayOrderItemDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }
        public int OrderId { get; set; }

    }
}
