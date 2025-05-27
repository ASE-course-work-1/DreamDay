using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Guest name must not exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "RSVP status must not exceed 50 characters.")]
        public string RSVP { get; set; } // e.g., Accepted, Declined, Pending

        [StringLength(100, ErrorMessage = "Meal preference must not exceed 100 characters.")]
        public string MealPreference { get; set; }

        [StringLength(50, ErrorMessage = "Seating arrangement must not exceed 50 characters.")]
        public string SeatingArrangement { get; set; }

        // Foreign key for Wedding
        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }
    }
}