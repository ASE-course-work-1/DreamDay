using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize]
    public class ChecklistController : Controller
    {
        private readonly DreamDayContext _context;

        public ChecklistController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Checklist/5
        public IActionResult Index(int weddingId)
        {
            var checklistItems = _context.ChecklistItems.Where(c => c.WeddingId == weddingId).ToList();
            ViewBag.WeddingId = weddingId;
            return View(checklistItems);
        }

        // GET: /Checklist/Create/5
        public IActionResult Create(int weddingId)
        {
            return View(new ChecklistItem { WeddingId = weddingId });
        }

        // POST: /Checklist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChecklistItem checklistItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingId = checklistItem.WeddingId });
            }
            return View(checklistItem);
        }

        // POST: /Checklist/Toggle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            var item = _context.ChecklistItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.IsCompleted = !item.IsCompleted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { weddingId = item.WeddingId });
        }
    }
}