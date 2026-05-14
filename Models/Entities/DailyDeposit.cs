using System.ComponentModel.DataAnnotations;

namespace LEGUZ.Models.Entities
{
    public class DailyDeposit
    {
        public int DailyDepositId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        // Store sales
        public decimal CumbresSales { get; set; } = 0;
        public decimal BallconesSales { get; set; } = 0;
        public decimal PaseoSales { get; set; } = 0;

        // Invoices
        public decimal CumbresInvoices { get; set; } = 0;
        public decimal BallconesInvoices { get; set; } = 0;
        public decimal PaseoInvoices { get; set; } = 0;
        public decimal CustomerInvoices { get; set; } = 0;
        public decimal CardPayments { get; set; } = 0;

        // Totals
        public decimal TotalToDeposit { get; set; } = 0;
        public decimal Difference { get; set; } = 0;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }
    }
}