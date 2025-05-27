using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Vendor name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Type must not exceed 50 characters.")]
        public string Type { get; set; } // e.g., Photographer, Florist, Caterer

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "Reviews must not exceed 1000 characters.")]
        public string Reviews { get; set; }

        // Navigation property for weddings using this vendor
        public virtual ICollection<WeddingVendor> WeddingVendors { get; set; } = new List<WeddingVendor>();
    }
}