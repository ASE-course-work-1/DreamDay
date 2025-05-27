using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace DreamDay.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DreamDayContext _context;

        public DashboardController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Dashboard
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _context.Users.Find(userId);

            if (user.Role == "Planner")
            {
                var weddings = _context.Weddings.ToList(); // Planners see all weddings
                return View(weddings);
            }
            else
            {
                var weddings = _context.Weddings.Where(w => w.UserId == userId).ToList();
                return View(weddings);
            }
        }
    }
}