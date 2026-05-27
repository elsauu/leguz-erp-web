using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class VentasController : Controller
    {
        private readonly LeguzContext _context;

        public VentasController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? fecha)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Ventas / Efectivo";

            var date = string.IsNullOrEmpty(fecha)
                ? DateOnly.FromDateTime(DateTime.Today)
                : DateOnly.Parse(fecha);

            var rutas = await _context.DeliveryRoutes
                .Where(r => r.IsActive)
                .OrderBy(r => r.Number)
                .ToListAsync();

            var registros = await _context.DailySalesRecords
                .Include(r => r.Salesperson)
                .Where(r => r.Date == date)
                .ToListAsync();

            var vendedoresPorRuta = (await _context.Salespersons
                .Where(s => s.IsActive && s.DeliveryRouteId != null && s.Type == "ROUTE")
                .ToListAsync())
                .GroupBy(s => s.DeliveryRouteId!.Value)
                .ToDictionary(g => g.Key, g => g.First());

            ViewBag.Fecha = date;
            ViewBag.Rutas = rutas;
            ViewBag.Registros = registros;
            ViewBag.VendedoresPorRuta = vendedoresPorRuta;

            return View();
        }

        public async Task<IActionResult> Capturar(int rutaId, string? fecha)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            var date = string.IsNullOrEmpty(fecha)
                ? DateOnly.FromDateTime(DateTime.Today)
                : DateOnly.Parse(fecha);

            var ruta = await _context.DeliveryRoutes.FindAsync(rutaId);
            if (ruta == null) return NotFound();

            var existing = await _context.DailySalesRecords
                .FirstOrDefaultAsync(r => r.Date == date && r.DeliveryRouteId == rutaId);

            if (existing != null)
                return RedirectToAction("Editar", new { id = existing.DailySalesRecordId });

            ViewData["Title"] = $"Capturar Liquidación — Ruta {ruta.Number:D2}";
            await CargarViewBag(ruta, date, null);

            var record = new DailySalesRecord { Date = date, DeliveryRouteId = rutaId };
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Capturar(DailySalesRecord record)
        {
            ModelState.Remove("DeliveryRoute");
            ModelState.Remove("Salesperson");

            if (!ModelState.IsValid)
            {
                var ruta = await _context.DeliveryRoutes.FindAsync(record.DeliveryRouteId);
                ViewData["Title"] = $"Capturar Liquidación — Ruta {ruta?.Number:D2}";
                await CargarViewBag(ruta!, record.Date, record.SalespersonId);
                return View(record);
            }

            CalcularFaltante(record);
            record.CreatedAt = DateTime.Now;
            record.CreatedBy = HttpContext.Session.GetString("UserFullName");
            _context.DailySalesRecords.Add(record);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Liquidación capturada correctamente.";
            return RedirectToAction("Index", new { fecha = record.Date.ToString("yyyy-MM-dd") });
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            var record = await _context.DailySalesRecords
                .Include(r => r.DeliveryRoute)
                .FirstOrDefaultAsync(r => r.DailySalesRecordId == id);

            if (record == null) return NotFound();

            ViewData["Title"] = $"Editar Liquidación — Ruta {record.DeliveryRoute.Number:D2}";
            await CargarViewBag(record.DeliveryRoute, record.Date, record.SalespersonId);
            return View("Capturar", record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, DailySalesRecord record)
        {
            if (id != record.DailySalesRecordId) return NotFound();

            ModelState.Remove("DeliveryRoute");
            ModelState.Remove("Salesperson");

            if (!ModelState.IsValid)
            {
                var ruta = await _context.DeliveryRoutes.FindAsync(record.DeliveryRouteId);
                ViewData["Title"] = $"Editar Liquidación — Ruta {ruta?.Number:D2}";
                await CargarViewBag(ruta!, record.Date, record.SalespersonId);
                return View("Capturar", record);
            }

            CalcularFaltante(record);
            _context.Update(record);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Liquidación actualizada correctamente.";
            return RedirectToAction("Index", new { fecha = record.Date.ToString("yyyy-MM-dd") });
        }

        private async Task CargarViewBag(DeliveryRoute ruta, DateOnly fecha, int? selectedSalespersonId)
        {
            var vendedores = await _context.Salespersons
                .Where(s => s.IsActive)
                .OrderBy(s => s.FirstName)
                .ToListAsync();

            ViewBag.Ruta = ruta;
            ViewBag.Fecha = fecha;
            ViewBag.Vendedores = new SelectList(
                vendedores.Select(s => new {
                    s.SalespersonId,
                    Nombre = s.FirstName + (string.IsNullOrEmpty(s.LastName) ? "" : " " + s.LastName)
                }),
                "SalespersonId", "Nombre", selectedSalespersonId);
        }

        private static void CalcularFaltante(DailySalesRecord r)
        {
            r.CashShortage = r.TotalSale - (r.Bills + r.Coins + r.CheckPayment + r.Expenses);
        }
    }
}
