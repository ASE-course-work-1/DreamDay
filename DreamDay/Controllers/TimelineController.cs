using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize]
    public class TimelineController : Controller
    {
        private readonly DreamDayContext _context;

        public TimelineController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Timeline/5
        public IActionResult Index(int weddingId)
        {
            var timelineEvents = _context.TimelineEvents.Where(t => t.WeddingId == weddingId).OrderBy(t => t.Time).ToList();
            ViewBag.WeddingId = weddingId;
            return View(timelineEvents);
        }

        // GET: /Timeline/Create/5
        public IActionResult Create(int weddingId)
        {
            return View(new TimelineEvent { WeddingId = weddingId });
        }

        // POST: /Timeline/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimelineEvent timelineEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timelineEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingId = timelineEvent.WeddingId });
            }
            return View(timelineEvent);
        }
    }
}