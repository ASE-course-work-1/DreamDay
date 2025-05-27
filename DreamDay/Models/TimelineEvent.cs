using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class TimelineEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Event name must not exceed 100 characters.")]
        public string EventName { get; set; } // e.g., Ceremony, Reception

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [StringLength(200, ErrorMessage = "Description must not exceed 200 characters.")]
        public string Description { get; set; }

        // Foreign key for Wedding
        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }
    }
}