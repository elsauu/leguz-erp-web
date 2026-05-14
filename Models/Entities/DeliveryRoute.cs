using System.ComponentModel.DataAnnotations;

namespace LEGUZ.Models.Entities
{
    public class DeliveryRoute
    {
        public int DeliveryRouteId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Salesperson> Salespersons { get; set; } = new List<Salesperson>();
    }
}