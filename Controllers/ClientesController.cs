using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LeguzContext _context;

        public ClientesController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Clientes";

            var customers = await _context.Customers
                .Include(c => c.DeliveryRoute)
                .OrderBy(c => c.CustomerType)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return View(customers);
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Nuevo Cliente";
            ViewBag.Routes = await GetRoutesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Routes = await GetRoutesSelectList();
                return View(customer);
            }

            if (customer.CustomerType != "ROUTE")
                customer.DeliveryRouteId = null;

            customer.CreatedAt = DateTime.Now;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Editar Cliente";

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            ViewBag.Routes = await GetRoutesSelectList(customer.DeliveryRouteId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Routes = await GetRoutesSelectList(customer.DeliveryRouteId);
                return View(customer);
            }

            if (customer.CustomerType != "ROUTE")
                customer.DeliveryRouteId = null;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            customer.IsActive = !customer.IsActive;
            await _context.SaveChangesAsync();

            TempData["Success"] = customer.IsActive ? "Cliente activado." : "Cliente desactivado.";
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
