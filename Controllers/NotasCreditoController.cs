using LEGUZ.Data;
using LEGUZ.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class NotasCreditoController : Controller
    {
        private readonly LeguzContext _context;

        public NotasCreditoController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? status)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Notas de Crédito";
            ViewData["CurrentStatus"] = status ?? "ALL";

            var query = _context.CreditNotes
                .Include(c => c.Salesperson)
                .Include(c => c.DeliveryRoute)
                .Include(c => c.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && status != "ALL")
                query = query.Where(c => c.Status == status);

            var notes = await query
                .OrderByDescending(c => c.Date)
                .ThenByDescending(c => c.CreditNoteId)
                .ToListAsync();

            return View(notes);
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Nueva Nota de Crédito";
            await LoadDropdowns();
            return View(new CreditNote { Date = DateOnly.FromDateTime(DateTime.Today) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreditNote creditNote)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(creditNote);
            }

            creditNote.CreatedAt = DateTime.Now;
            creditNote.CreatedBy = HttpContext.Session.GetString("Username");

            _context.CreditNotes.Add(creditNote);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Nota de crédito registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Editar Nota de Crédito";

            var note = await _context.CreditNotes.FindAsync(id);
            if (note == null) return NotFound();

            await LoadDropdowns();
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreditNote creditNote)
        {
            if (id != creditNote.CreditNoteId) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(creditNote);
            }

            var existing = await _context.CreditNotes.FindAsync(id);
            if (existing == null) return NotFound();

            existing.FolioNumber = creditNote.FolioNumber;
            existing.Date = creditNote.Date;
            existing.StoreName = creditNote.StoreName;
            existing.Packages = creditNote.Packages;
            existing.Kilos = creditNote.Kilos;
            existing.Dough = creditNote.Dough;
            existing.Crackling = creditNote.Crackling;
            existing.Chips = creditNote.Chips;
            existing.Taco = creditNote.Taco;
            existing.Status = creditNote.Status;
            existing.IsBarbacoa = creditNote.IsBarbacoa;
            existing.Notes = creditNote.Notes;
            existing.SalespersonId = creditNote.SalespersonId;
            existing.DeliveryRouteId = creditNote.DeliveryRouteId;
            existing.CustomerId = creditNote.CustomerId;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Nota de crédito actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            var allowed = new[] { "PENDING", "SETTLED", "CANCELLED" };
            if (!allowed.Contains(status))
                return BadRequest();

            var note = await _context.CreditNotes.FindAsync(id);
            if (note == null) return NotFound();

            note.Status = status;
            await _context.SaveChangesAsync();

            TempData["Success"] = status switch
            {
                "SETTLED" => "Nota marcada como liquidada.",
                "CANCELLED" => "Nota cancelada.",
                _ => "Nota marcada como pendiente."
            };
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns()
        {
            ViewBag.Salespersons = new SelectList(
                await _context.Salespersons
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.FirstName)
                    .ToListAsync(),
                "SalespersonId",
                "FirstName");

            ViewBag.DeliveryRoutes = new SelectList(
                await _context.DeliveryRoutes
                    .Where(r => r.IsActive)
                    .OrderBy(r => r.Number)
                    .ToListAsync(),
                "DeliveryRouteId",
                "Name");

            ViewBag.Customers = new SelectList(
                await _context.Customers
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Name)
                    .ToListAsync(),
                "CustomerId",
                "Name");
        }
    }
}
