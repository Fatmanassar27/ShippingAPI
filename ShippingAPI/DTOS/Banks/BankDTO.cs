using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS
{
    public class BankDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(100)]
        public string? BranchName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(15,2)")]
        public decimal Balance { get; set; } = 0;

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
}