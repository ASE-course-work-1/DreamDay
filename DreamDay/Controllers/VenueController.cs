using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DreamDay.Controllers
{
    public class VenueController : Controller
    {
        private readonly DreamDayContext _context;

        public VenueController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Venue
        public IActionResult Index()
        {
            var venues = _context.Venues.ToList();
            return View(venues);
        }

        // GET: /Venue/Details/5
        public IActionResult Details(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // GET: /Venue/Create
        [Authorize(Roles = "Admin,Planner")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Planner")]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
    }
}