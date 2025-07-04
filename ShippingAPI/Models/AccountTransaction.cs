using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class AccountTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Notes { get; set; } = string.Empty;

        [Column(TypeName = "decimal(15,2)")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public bool IsCompanyShare { get; set; } = false;

        [ForeignKey("User")]
        public string? UserId { get; set; }

        [ForeignKey("Bank")]
        public int? BankId { get; set; }

        [ForeignKey("Safe")]
        public int? SafeId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Bank? Bank { get; set; }
        public virtual Safe? Safe { get; set; }
    }
}
