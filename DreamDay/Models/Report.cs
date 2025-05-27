using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Report type must not exceed 100 characters.")]
        public string ReportType { get; set; } // e.g., VenuePopularity, BudgetAnalysis

        [Required]
        public string ReportData { get; set; } // JSON or serialized data for report content

        [DataType(DataType.DateTime)]
        public DateTime GeneratedAt { get; set; }

        // Foreign key for Planner (User)
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}