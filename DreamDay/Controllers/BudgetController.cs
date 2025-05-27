using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly DreamDayContext _context;

        public BudgetController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Budget/5
        public IActionResult Index(int weddingId)
        {
            var budgetItems = _context.BudgetItems.Where(b => b.WeddingId == weddingId).ToList();
            var wedding = _context.Weddings.Find(weddingId);
            ViewBag.WeddingId = weddingId;
            ViewBag.TotalBudget = wedding?.Budget ?? 0;
            ViewBag.TotalSpent = budgetItems.Sum(b => b.SpentAmount);
            return View(budgetItems);
        }

        // GET: /Budget/Create/5
        public IActionResult Create(int weddingId)
        {
            return View(new BudgetItem { WeddingId = weddingId });
        }

        // POST: /Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetItem budgetItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { weddingId = budgetItem.WeddingId });
            }
            return View(budgetItem);
        }
    }
}