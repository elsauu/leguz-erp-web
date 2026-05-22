using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class RutasController : Controller
    {
        private readonly LeguzContext _context;

        public RutasController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Rutas";

            var routes = await _context.DeliveryRoutes
                .Include(r => r.Salespersons)
                .OrderBy(r => r.Number)
                .ToListAsync();

            return View(routes);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Nueva Ruta";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryRoute route)
        {
            if (!ModelState.IsValid)
                return View(route);

            _context.DeliveryRoutes.Add(route);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Ruta creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Editar Ruta";

            var route = await _context.DeliveryRoutes.FindAsync(id);
            if (route == null) return NotFound();

            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeliveryRoute route)
        {
            if (id != route.DeliveryRouteId) return NotFound();

            if (!ModelState.IsValid)
                return View(route);

            _context.Update(route);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Ruta actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            var route = await _context.DeliveryRoutes.FindAsync(id);
            if (route == null) return NotFound();

            route.IsActive = !route.IsActive;
            await _context.SaveChangesAsync();

            TempData["Success"] = route.IsActive ? "Ruta activada." : "Ruta desactivada.";
            return RedirectToAction(nameof(Index));
        }
    }
}