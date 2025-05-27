using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public override string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public override string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public required string Role { get; set; }

        public virtual ICollection<Wedding> Weddings { get; set; } = new List<Wedding>();
    }
}