using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize]
    public class GuestListController : Controller
    {
        private readonly DreamDayContext _context;

        public GuestListController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /GuestList/5
        public IActionResult Index(int weddingId)
        {
            var guests = _context.Guests.Where(g => g.WeddingId == weddingId).ToList();
            ViewBag.WeddingId = weddingId;
            return View(guests);
        }

        // GET: /GuestList/Create/5
        public IActionResult Create(int weddingId)
        {
            return View(new Guest { WeddingId = weddingId });
        }

        // POST: /GuestList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingId = guest.WeddingId });
            }
            return View(guest);
        }

        // GET: /GuestList/Edit/5
        public IActionResult Edit(int id)
        {
            var guest = _context.Guests.Find(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: /GuestList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Update(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingId = guest.WeddingId });
            }
            return View(guest);
        }
    }
}