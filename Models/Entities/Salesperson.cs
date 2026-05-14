using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class Salesperson
    {
        public int SalespersonId { get; set; }

        [Required]
        [MaxLength(80)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(80)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = "ROUTE";

        public bool IsActive { get; set; } = true;

        [MaxLength(15)]
        public string? Phone { get; set; }

        // Foreign key
        public int? DeliveryRouteId { get; set; }

        [ForeignKey("DeliveryRouteId")]
        public DeliveryRoute? DeliveryRoute { get; set; }
    }
}