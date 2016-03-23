using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Model;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Application
{
    public class TravelMNContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<Cidade> Cidade { get; set; }

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
