using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class ReportController : Controller
    {
        private readonly DreamDayContext _context;

        public ReportController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Report
        public IActionResult Index()
        {
            var reports = _context.Reports.ToList();
            return View(reports);
        }

        // GET: /Report/GenerateVenuePopularity
        public async Task<IActionResult> GenerateVenuePopularity()
        {
            var venueData = _context.Weddings
                .GroupBy(w => w.VenueId)
                .Select(g => new { VenueId = g.Key, Count = g.Count() })
                .Join(_context.Venues,
                    g => g.VenueId,
                    v => v.Id,
                    (g, v) => new { v.Name, g.Count })
                .ToList();

            var report = new Report
            {
                ReportType = "VenuePopularity",
                ReportData = JsonSerializer.Serialize(venueData),
                GeneratedAt = DateTime.Now,
                UserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}