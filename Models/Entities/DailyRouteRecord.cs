using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class DailyRouteRecord
    {
        public int DailyRouteRecordId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        // Load (what the salesperson takes)
        public decimal LoadHotKilos { get; set; } = 0;
        public decimal LoadHalfKilo { get; set; } = 0;
        public decimal LoadTacoKilo { get; set; } = 0;
        public decimal LoadPackages { get; set; } = 0;
        public decimal LoadHalfPackage { get; set; } = 0;
        public decimal LoadTacoPackage { get; set; } = 0;
        public decimal LoadChips { get; set; } = 0;
        public decimal LoadDough { get; set; } = 0;
        public decimal LoadCrackling { get; set; } = 0;
        public decimal LoadPresentation { get; set; } = 0;

        // Table (what was registered as sold)
        public decimal TableHotKilos { get; set; } = 0;
        public decimal TablePackages { get; set; } = 0;
        public decimal TableChips { get; set; } = 0;
        public decimal TableDough { get; set; } = 0;
        public decimal TableCrackling { get; set; } = 0;

        // Summary (returns and shortages)
        public decimal KilosSold { get; set; } = 0;
        public decimal KilosReturned { get; set; } = 0;
        public decimal PackagesSold { get; set; } = 0;
        public decimal PackagesReturned { get; set; } = 0;
        public decimal ChipsSold { get; set; } = 0;
        public decimal ChipsReturned { get; set; } = 0;
        public decimal DoughSold { get; set; } = 0;
        public decimal DoughReturned { get; set; } = 0;
        public decimal CracklingsSold { get; set; } = 0;
        public decimal ShortageKilos { get; set; } = 0;
        public decimal ShortagePackages { get; set; } = 0;

        public bool SalespersonSigned { get; set; } = false;

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