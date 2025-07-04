using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.DTOS.Saves
{
    public class SavesDto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "Money")]
        public decimal Balance { get; set; } = 0;

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int BranchId { get; set; }

        [StringLength(100)]
        public string? BranchName { get; set; } = string.Empty;
     
    }
}
