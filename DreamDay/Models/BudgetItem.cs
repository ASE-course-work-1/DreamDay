using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class BudgetItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Category must not exceed 50 characters.")]
        public string Category { get; set; } // e.g., Venue, Catering

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Allocated amount must be a positive value.")]
        public decimal AllocatedAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Spent amount must be a positive value.")]
        public decimal SpentAmount { get; set; }

        // Foreign key for Wedding
        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }
    }
}