using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class DepositosController : Controller
    {
        private readonly LeguzContext _context;

        public DepositosController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Depósitos";

            var deposits = await _context.DailyDeposits
                .OrderByDescending(d => d.Date)
                .ThenByDescending(d => d.DailyDepositId)
                .Take(60)
                .ToListAsync();

            return View(deposits);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Nuevo Depósito";

            var deposit = new DailyDeposit { Date = DateOnly.FromDateTime(DateTime.Today) };
            return View(deposit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyDeposit deposit)
        {
            if (!ModelState.IsValid)
                return View(deposit);

            var duplicate = await _context.DailyDeposits
                .AnyAsync(d => d.Date == deposit.Date);

            if (duplicate)
            {
                ModelState.AddModelError("Date", "Ya existe un depósito registrado para esta fecha.");
                return View(deposit);
            }

            Calcular(deposit);
            deposit.CreatedAt = DateTime.Now;
            deposit.CreatedBy = HttpContext.Session.GetString("UserFullName");

            _context.DailyDeposits.Add(deposit);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Depósito registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Editar Depósito";

            var deposit = await _context.DailyDeposits.FindAsync(id);
            if (deposit == null) return NotFound();

            return View(deposit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DailyDeposit deposit)
        {
            if (id != deposit.DailyDepositId) return NotFound();

            if (!ModelState.IsValid)
                return View(deposit);

            var existing = await _context.DailyDeposits.FindAsync(id);
            if (existing == null) return NotFound();

            if (existing.Date != deposit.Date)
            {
                var clash = await _context.DailyDeposits
                    .AnyAsync(d => d.Date == deposit.Date && d.DailyDepositId != id);
                if (clash)
                {
                    ModelState.AddModelError("Date", "Ya existe un depósito registrado para esta fecha.");
                    return View(deposit);
                }
            }

            existing.Date = deposit.Date;
            existing.CumbresSales = deposit.CumbresSales;
            existing.BalconesSales = deposit.BalconesSales;
            existing.PaseoSales = deposit.PaseoSales;
            existing.CumbresInvoices = deposit.CumbresInvoices;
            existing.BalconesInvoices = deposit.BalconesInvoices;
            existing.PaseoInvoices = deposit.PaseoInvoices;
            existing.CustomerInvoices = deposit.CustomerInvoices;
            existing.CardPayments = deposit.CardPayments;
            existing.Notes = deposit.Notes;

            Calcular(existing);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Depósito actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private static void Calcular(DailyDeposit d)
        {
            d.TotalToDeposit = d.CumbresSales + d.BalconesSales + d.PaseoSales;
            d.Difference = d.TotalToDeposit
                - (d.CumbresInvoices + d.BalconesInvoices + d.PaseoInvoices
                   + d.CustomerInvoices + d.CardPayments);
        }
    }
}
