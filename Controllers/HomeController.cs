using LEGUZ.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LEGUZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly LeguzContext _context;

        public HomeController(LeguzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Auth");

            ViewData["Title"] = "Dashboard";

            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfWeek = today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

            // KPIs
            ViewBag.ActiveRoutes = await _context.DeliveryRoutes.CountAsync(r => r.IsActive);
            ViewBag.ActiveSalespersons = await _context.Salespersons.CountAsync(s => s.IsActive);
            ViewBag.TodayRecords = await _context.DailyRouteRecords.CountAsync(r => r.Date == today);
            ViewBag.PendingCreditNotes = await _context.CreditNotes.CountAsync(c => c.Status == "PENDING");

            // Today's sales total
            ViewBag.TodaySalesTotal = await _context.DailySalesRecords
                .Where(s => s.Date == today)
                .SumAsync(s => (decimal?)s.TotalSale) ?? 0;

            // Weekly sales per route (last 7 days)
            var weeklyData = await _context.DailySalesRecords
                .Where(s => s.Date >= startOfWeek && s.Date <= today)
                .Include(s => s.DeliveryRoute)
                .GroupBy(s => s.DeliveryRoute!.Name)
                .Select(g => new { Route = g.Key, Total = g.Sum(s => s.TotalSale) })
                .OrderByDescending(g => g.Total)
                .Take(10)
                .ToListAsync();

            ViewBag.WeeklyLabels = weeklyData.Select(w => w.Route).ToList();
            ViewBag.WeeklyTotals = weeklyData.Select(w => w.Total).ToList();

            // Recent credit notes
            ViewBag.RecentCreditNotes = await _context.CreditNotes
                .Include(c => c.DeliveryRoute)
                .Include(c => c.Salesperson)
                .OrderByDescending(c => c.Date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}