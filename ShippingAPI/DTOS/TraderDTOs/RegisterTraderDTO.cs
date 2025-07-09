using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.TraderDTOs
{
    public class RegisterTraderDTO
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

        // بيانات التاجر
        [Required]
        public string StoreName { get; set; }

        [Required]
        public int GovernorateId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int BranchId { get; set; }

        public decimal? CustomPickupCost { get; set; } = 0;

        [Range(0, 100)]
        public decimal RejectedOrderShippingShare { get; set; } = 0;
    }
}
