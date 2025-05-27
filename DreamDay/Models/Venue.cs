using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Venue name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Location must not exceed 200 characters.")]
        public string Location { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; }

        // Navigation property for weddings at this venue
        public virtual ICollection<Wedding> Weddings { get; set; } = new List<Wedding>();
    }
}