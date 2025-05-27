using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public override string UserName { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public override string Email { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = default!;

        public virtual ICollection<Wedding> Weddings { get; set; } = new List<Wedding>();
    }
}