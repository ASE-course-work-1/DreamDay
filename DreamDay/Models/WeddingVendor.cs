using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class WeddingVendor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}