using estacionamento.Data.Mappings;
using estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace estacionamento.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Establishment> Establishments { get; set; }
        public DbSet<InOutEstablishment> InOutEstablishments { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstablishmentMap());
            modelBuilder.ApplyConfiguration(new VehicleMap());
            modelBuilder.ApplyConfiguration(new InOutEstablishmentMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        
        }
    }

}

