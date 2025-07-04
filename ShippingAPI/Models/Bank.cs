using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(15,2)")]
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
        [Required]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch? Branch { get; set; }
        [InverseProperty("SourceBank")]
        public virtual ICollection<FinancialTransfer> SourceTransfers { get; set; } = new List<FinancialTransfer>();

        [InverseProperty("DestinationBank")]
        public virtual ICollection<FinancialTransfer> DestinationTransfers { get; set; } = new List<FinancialTransfer>();
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new List<AccountTransaction>();

    }
}
