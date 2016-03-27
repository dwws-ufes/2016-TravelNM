using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistence
{
    public class TravelMNContext : DbContext
    {
        public DbSet<City> City { get; set; }

        public DbSet<User> Users { get; set; }

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
