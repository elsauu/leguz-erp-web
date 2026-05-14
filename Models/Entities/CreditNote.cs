using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class CreditNote
    {
        public int CreditNoteId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FolioNumber { get; set; } = string.Empty;

        [Required]
        public DateOnly Date { get; set; }

        [MaxLength(80)]
        public string? StoreName { get; set; }

        public decimal Packages { get; set; } = 0;
        public decimal Kilos { get; set; } = 0;
        public decimal Dough { get; set; } = 0;
        public decimal Crackling { get; set; } = 0;
        public decimal Chips { get; set; } = 0;
        public decimal Taco { get; set; } = 0;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "PENDING"; // PENDING, SETTLED, CANCELLED

        public bool IsBarbacoa { get; set; } = false;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        // Foreign keys
        public int? SalespersonId { get; set; }

        [ForeignKey("SalespersonId")]
        public Salesperson? Salesperson { get; set; }

        public int? DeliveryRouteId { get; set; }

        [ForeignKey("DeliveryRouteId")]
        public DeliveryRoute? DeliveryRoute { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
    }
}