using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class ChecklistItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Task description must not exceed 200 characters.")]
        public string Task { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        // Foreign key for Wedding
        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }
    }
}