using LEGUZ.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class AuthController : Controller
    {
        private readonly LeguzContext _context;

        public AuthController(LeguzContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserId") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            // Save session
            HttpContext.Session.SetString("UserId", user.AppUserId.ToString());
            HttpContext.Session.SetString("UserFullName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("Username", user.Username);

            // Update last login
            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}