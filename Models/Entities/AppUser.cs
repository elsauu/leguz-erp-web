using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEGUZ.Models.Entities
{
    public class AppUser
    {
        public int AppUserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "VIEWER"; // ADMIN, OPERATOR, SALESPERSON, VIEWER

        public bool IsActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key (optional - link to salesperson)
        public int? SalespersonId { get; set; }

        [ForeignKey("SalespersonId")]
        public Salesperson? Salesperson { get; set; }
    }
}