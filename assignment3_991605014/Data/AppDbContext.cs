using assignment3_991605014.Models;
using Microsoft.EntityFrameworkCore;
namespace assignment3_991605014.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TransportRoute> Routes { get; set; }
        public DbSet<TruckWorkshop> TruckWorkshops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify precision for the RPayPerKm property in the TransportRoute entity
            modelBuilder.Entity<TransportRoute>()
                .Property(r => r.RPayPerKm)
                .HasPrecision(18, 2);  // Sets the precision to 18 and the scale to 2
        }
    }
}