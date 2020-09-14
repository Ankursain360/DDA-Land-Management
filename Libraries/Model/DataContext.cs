using Libraries.Model.Entity;
using Libraries.Model.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Libraries.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<District> District { get; set; }

        public virtual DbSet<Zone> Zone { get; set; }
        public virtual DbSet<Village> Village { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<LandNotification> Notification { get; set; }
        public virtual DbSet<Interest> Interest { get; set; }
        public virtual DbSet<PropertyType> PropertyType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }

        public virtual DbSet<Rate> Rate { get; set; }

        public virtual DbSet<Rebate> Rebate { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<PageRole> PageRole { get; set; }
        public virtual DbSet<Nazulland> Nazulland { get; set; }
        public virtual DbSet<Tehsil> Tehsil { get; set; }
        public virtual DbSet<Villagetype> Villagetype { get; set; }
        public virtual DbSet<Acquiredlandvillage> Acquiredlandvillage { get; set; }
        public virtual DbSet<Classificationofland> Classificationofland { get; set; }
        public virtual DbSet<Disposaltype> Disposaltype { get; set; }
        public virtual DbSet<Landuse> Landuse { get; set; }
        public virtual DbSet<Propertyregistration> Propertyregistration { get; set; }

        public virtual DbSet<Proposaldetails> Proposaldetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new DesignationConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new VillageConfiguration());
            modelBuilder.ApplyConfiguration(new DivisionConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new PageConfiguration());
            modelBuilder.ApplyConfiguration(new InterestConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new LocalityConfiguration());
            modelBuilder.ApplyConfiguration(new RateConfiguration());
            modelBuilder.ApplyConfiguration(new RebateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PageRoleConfiguration());
            modelBuilder.ApplyConfiguration(new NazullandConfiguration());
            modelBuilder.ApplyConfiguration(new ClassificationoflandConfiguration());
            modelBuilder.ApplyConfiguration(new LanduseConfiguration());
            modelBuilder.ApplyConfiguration(new DisposaltypeConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyregistrationConfiguration());
            modelBuilder.ApplyConfiguration(new ProposaldetailsConfiguration());
        }
    }
}