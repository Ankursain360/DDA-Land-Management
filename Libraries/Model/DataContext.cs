using Libraries.Model.Entity;
using Libraries.Model.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Model
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base (options)
        {
            
        }

        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<Country> Country { get; set; }

        public virtual DbSet<Designation> TblMasterDesignation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new DesignationConfiguration());
        }
    }
}