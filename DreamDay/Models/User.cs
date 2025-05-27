using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class User : IdentityUser<int> // Use int as the primary key type
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public override string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public override string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Possible values: Couple, Planner, Admin

        // Navigation property for weddings associated with the user
        public virtual ICollection<Wedding> Weddings { get; set; } = new List<Wedding>();
    }
}