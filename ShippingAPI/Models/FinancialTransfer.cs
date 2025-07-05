using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class FinancialTransfer
    {
        [Key]
        public int Id { get; set; }

        // source
        public int? SourceBankId { get; set; }
        public int? SourceSafeId { get; set; }

        //destination
        public int? DestinationBankId { get; set; }
        public int? DestinationSafeId { get; set; }
        public string? AdminId { get; set; }

        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? Note { get; set; }

        //Navigation Properties
        [ForeignKey(nameof(AdminId))]
        public virtual ApplicationUser? Admin { get; set; }

        [ForeignKey(nameof(SourceBankId))]
        public virtual Bank? SourceBank { get; set; }

        [ForeignKey(nameof(SourceSafeId))]
        public virtual Safe? SourceSafe { get; set; }

        [ForeignKey(nameof(DestinationBankId))]
        public virtual Bank? DestinationBank { get; set; }

        [ForeignKey(nameof(DestinationSafeId))]
        public virtual Safe? DestinationSafe { get; set; }
    }
}
