using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DreamDay.Data;
using DreamDay.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Controllers
{
    public class AccountController : Controller
    {
        private readonly DreamDayContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(DreamDayContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Required] string username, [Required] string email, [Required] string password, [Required] string role)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var user = new User { UserName = username, Email = email, Role = role };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Dashboard");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Registration failed: {ex.Message}");
            }
            return View();
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required] string username, [Required] string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Login failed: {ex.Message}");
            }
            return View();
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}