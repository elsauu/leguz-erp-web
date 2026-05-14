using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class DailySalesRecord
    {
        public int DailySalesRecordId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public decimal TotalSale { get; set; } = 0;
        public decimal Bills { get; set; } = 0;
        public decimal Coins { get; set; } = 0;
        public decimal CheckPayment { get; set; } = 0;
        public decimal CashShortage { get; set; } = 0;
        public decimal Expenses { get; set; } = 0;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        // Foreign keys
        public int DeliveryRouteId { get; set; }

        [ForeignKey("DeliveryRouteId")]
        public DeliveryRoute DeliveryRoute { get; set; } = null!;

        public int? SalespersonId { get; set; }

        [ForeignKey("SalespersonId")]
        public Salesperson? Salesperson { get; set; }
    }
}