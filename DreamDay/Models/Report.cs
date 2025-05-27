using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Report type must not exceed 100 characters.")]
        public string ReportType { get; set; } = default!; // e.g., VenuePopularity, BudgetAnalysis

        [Required]
        public string ReportData { get; set; } = default!; // JSON or serialized data for report content

        public DateTime GeneratedAt { get; set; }

        // Foreign key for Planner (User)
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}