using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.RejectionReasonDTOs
{
    public class displayRejectionReasonDTO
    {
        public int Id { get; set; }
        public string Reason { get; set; }
    }
}
