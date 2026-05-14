using Microsoft.EntityFrameworkCore;
using LEGUZ.Models.Entities;

namespace LEGUZ.Data
{
    public class LeguzContext : DbContext
    {
        public LeguzContext(DbContextOptions<LeguzContext> options) : base(options) { }

        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }
        public DbSet<Salesperson> Salespersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data - 10 distribution routes
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
        }
    }
}