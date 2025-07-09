using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.TraderDTOs
{
    public class UpdateTraderDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }


        public string StoreName { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int BranchId { get; set; }
        public decimal CustomPickupCost { get; set; }
        public decimal RejectedOrderShippingShare { get; set; }
        public bool IsActive { get; set; }

    }
}
