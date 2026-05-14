using System.ComponentModel.DataAnnotations;

namespace LEGUZ.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty; // KG, PACKAGE, PIECE

        [Required]
        [MaxLength(30)]
        public string Category { get; set; } = string.Empty; // HOT, COLD, CHIPS, DOUGH, CRACKLING, PRESENTATION

        public decimal UnitPrice { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}