using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly LeguzContext _context;

        public VendedoresController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Vendedores";

            var salespersons = await _context.Salespersons
                .Include(s => s.DeliveryRoute)
                .OrderBy(s => s.Type)
                .ThenBy(s => s.FirstName)
                .ToListAsync();

            return View(salespersons);
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Nuevo Vendedor";
            ViewBag.Routes = await GetRoutesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salesperson salesperson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Routes = await GetRoutesSelectList();
                return View(salesperson);
            }

            if (salesperson.Type != "ROUTE")
                salesperson.DeliveryRouteId = null;

            _context.Salespersons.Add(salesperson);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Vendedor registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Editar Vendedor";

            var salesperson = await _context.Salespersons.FindAsync(id);
            if (salesperson == null) return NotFound();

            ViewBag.Routes = await GetRoutesSelectList(salesperson.DeliveryRouteId);
            return View(salesperson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Salesperson salesperson)
        {
            if (id != salesperson.SalespersonId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Routes = await GetRoutesSelectList(salesperson.DeliveryRouteId);
                return View(salesperson);
            }

            if (salesperson.Type != "ROUTE")
                salesperson.DeliveryRouteId = null;

            _context.Update(salesperson);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Vendedor actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            var salesperson = await _context.Salespersons.FindAsync(id);
            if (salesperson == null) return NotFound();

            salesperson.IsActive = !salesperson.IsActive;
            await _context.SaveChangesAsync();

            TempData["Success"] = salesperson.IsActive ? "Vendedor activado." : "Vendedor desactivado.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> GetRoutesSelectList(int? selectedId = null)
        {
            var routes = await _context.DeliveryRoutes
                .Where(r => r.IsActive)
                .OrderBy(r => r.Number)
                .ToListAsync();
            return new SelectList(routes, "DeliveryRouteId", "Name", selectedId);
        }
    }
}
