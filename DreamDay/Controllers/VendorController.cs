using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DreamDay.Controllers
{
    public class VendorController : Controller
    {
        private readonly DreamDayContext _context;

        public VendorController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Vendor
        public IActionResult Index()
        {
            var vendors = _context.Vendors.ToList();
            return View(vendors);
        }

        // GET: /Vendor/Details/5
        public IActionResult Details(int id)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // GET: /Vendor/Create
        [Authorize(Roles = "Admin,Planner")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Vendor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Planner")]
        public async Task<IActionResult> Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }
    }
}