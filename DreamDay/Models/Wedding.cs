using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Wedding
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Wedding name must not exceed 100 characters.")]
        public string Name { get; set; } // e.g., "John & Jane's Wedding"

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value.")]
        public decimal Budget { get; set; }

        // Foreign key for User (Couple or Planner)
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Foreign key for Venue
        [ForeignKey("Venue")]
        public int? VenueId { get; set; }
        public virtual Venue Venue { get; set; }

        // Navigation properties for related entities
        public virtual ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
        public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
        public virtual ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();
        public virtual ICollection<TimelineEvent> TimelineEvents { get; set; } = new List<TimelineEvent>();
        public virtual ICollection<WeddingVendor> WeddingVendors { get; set; } = new List<WeddingVendor>();
    }
}