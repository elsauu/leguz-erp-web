using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string CustomerType { get; set; } = string.Empty; // ROUTE, SUPERMARKET, MOTORCYCLE, STORE

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key
        public int? DeliveryRouteId { get; set; }

        [ForeignKey("DeliveryRouteId")]
        public DeliveryRoute? DeliveryRoute { get; set; }
    }
}