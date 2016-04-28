using System.Data.Entity;
using Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistence
{
    public class TravelMNContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TravelPackage> TravelPackages { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<TravelPackageBuy> TravelPackageBuys { get; set; }

        public TravelMNContext()
        {
            Database.SetInitializer<TravelMNContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
