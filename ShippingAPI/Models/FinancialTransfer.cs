using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Models
{
    public class FinancialTransfer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBank")]
        public int? SourceId { get; set; }

        [Required]
        [StringLength(20)]
        public string SourceType { get; set; } = string.Empty;

        [ForeignKey("DestinationBank")]
        public int? DestinationId { get; set; }

        [Required]
        [StringLength(20)]
        public string DestinationType { get; set; } = string.Empty;

        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? Note { get; set; }

        public virtual Bank? SourceBank { get; set; }
        public virtual Bank? DestinationBank { get; set; }

        public virtual Safe? SourceSafe { get; set; }
        public virtual Safe? DestinationSafe { get; set; }
    }
}
