using Microsoft.AspNetCore.Mvc;
using DreamDay.Data;
using DreamDay.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DreamDayContext _context;

        public AdminController(DreamDayContext context)
        {
            _context = context;
        }

        // GET: /Admin
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: /Admin/ManageUsers
        public IActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: /Admin/DeleteUser/5
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: /Admin/DeleteUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageUsers));
        }
    }
}