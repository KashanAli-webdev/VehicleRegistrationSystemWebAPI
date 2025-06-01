using Microsoft.EntityFrameworkCore;
using VehicleRegistrationSystemWebAPI.Models;

namespace VehicleRegistrationSystemWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
