using Microsoft.EntityFrameworkCore;
using LEGUZ.Models.Entities;

namespace LEGUZ.Data
{
    public class LeguzContext : DbContext
    {
        public LeguzContext(DbContextOptions<LeguzContext> options) : base(options) { }

        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }
        public DbSet<Salesperson> Salespersons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditNote> CreditNotes { get; set; }
        public DbSet<DailySalesRecord> DailySalesRecords { get; set; }
        public DbSet<DailyDeposit> DailyDeposits { get; set; }
        public DbSet<DailyRouteRecord> DailyRouteRecords { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints
            modelBuilder.Entity<DailyRouteRecord>()
                .HasIndex(r => new { r.Date, r.DeliveryRouteId })
                .IsUnique();

            modelBuilder.Entity<DailySalesRecord>()
                .HasIndex(r => new { r.Date, r.DeliveryRouteId })
                .IsUnique();

            modelBuilder.Entity<DailyDeposit>()
                .HasIndex(r => r.Date)
                .IsUnique();

            // Seed - 10 routes
            modelBuilder.Entity<DeliveryRoute>().HasData(
                new DeliveryRoute { DeliveryRouteId = 1, Number = 1, Name = "BALCONES RIOS" },
                new DeliveryRoute { DeliveryRouteId = 2, Number = 2, Name = "BALCONES LAGOS" },
                new DeliveryRoute { DeliveryRouteId = 3, Number = 3, Name = "LA JOYA" },
                new DeliveryRoute { DeliveryRouteId = 4, Number = 4, Name = "BUGAMBILIAS" },
                new DeliveryRoute { DeliveryRouteId = 5, Number = 5, Name = "ALMAGUER" },
                new DeliveryRoute { DeliveryRouteId = 6, Number = 6, Name = "LA JUAREZ" },
                new DeliveryRoute { DeliveryRouteId = 7, Number = 7, Name = "PASEO RINCON" },
                new DeliveryRoute { DeliveryRouteId = 8, Number = 8, Name = "VAMOS TAMAULIPAS" },
                new DeliveryRoute { DeliveryRouteId = 9, Number = 9, Name = "RANCHO GRANDE" },
                new DeliveryRoute { DeliveryRouteId = 10, Number = 10, Name = "PUERTA DEL SOL" }
            );

            // Seed - admin user (password: Admin123!)
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    AppUserId = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    FullName = "Administrador",
                    Role = "ADMIN",
                    IsActive = true
                }
            );
        }
    }
}