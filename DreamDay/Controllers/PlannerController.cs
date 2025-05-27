using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class PlannerController : Controller
    {
        private readonly DreamDayContext _context;

        public PlannerController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Planner
        public IActionResult Index()
        {
            var weddings = _context.Weddings.ToList();
            return View(weddings);
        }

        // GET: /Planner/ManageVendors/5
        public IActionResult ManageVendors(int weddingId)
        {
            var weddingVendors = _context.WeddingVendors.Where(wv => wv.WeddingId == weddingId)
                .Select(wv => wv.Vendor).ToList();
            ViewBag.WeddingId = weddingId;
            ViewBag.AvailableVendors = _context.Vendors.ToList();
            return View(weddingVendors);
        }

        // POST: /Planner/AddVendor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVendor(int weddingId, int vendorId)
        {
            if (!_context.WeddingVendors.Any(wv => wv.WeddingId == weddingId && wv.VendorId == vendorId))
            {
                _context.WeddingVendors.Add(new WeddingVendor { WeddingId = weddingId, VendorId = vendorId });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageVendors), new { weddingId });
        }
    }
}