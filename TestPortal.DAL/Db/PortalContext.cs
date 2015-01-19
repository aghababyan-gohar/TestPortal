using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Entities;

namespace TestPortal.DAL.Db
{
    public class PortalContext : DbContext
    {

        public PortalContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer<PortalContext>(new CreateDatabaseIfNotExists<PortalContext>());
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
