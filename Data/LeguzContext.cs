using Microsoft.EntityFrameworkCore;
using LEGUZ.Models.Entities;

namespace LEGUZ.Data
{
    public class LeguzContext : DbContext
    {
        public LeguzContext(DbContextOptions<LeguzContext> options) : base(options) { }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Salesperson> Salespersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data - 10 distribution routes
            modelBuilder.Entity<Route>().HasData(
                new Route { RouteId = 1, Number = 1, Name = "BALCONES RIOS" },
                new Route { RouteId = 2, Number = 2, Name = "BALCONES LAGOS" },
                new Route { RouteId = 3, Number = 3, Name = "LA JOYA" },
                new Route { RouteId = 4, Number = 4, Name = "BUGAMBILIAS" },
                new Route { RouteId = 5, Number = 5, Name = "ALMAGUER" },
                new Route { RouteId = 6, Number = 6, Name = "LA JUAREZ" },
                new Route { RouteId = 7, Number = 7, Name = "PASEO RINCON" },
                new Route { RouteId = 8, Number = 8, Name = "VAMOS TAMAULIPAS" },
                new Route { RouteId = 9, Number = 9, Name = "RANCHO GRANDE" },
                new Route { RouteId = 10, Number = 10, Name = "PUERTA DEL SOL" }
            );
        }
    }
}