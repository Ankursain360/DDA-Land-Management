using Libraries.Model.Entity;
using Libraries.Model.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model.EntityConfiguration;

namespace Libraries.Model
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Userprofile> Userprofile { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Natureofencroachment> Natureofencroachment { get; set; }

        public virtual DbSet<Zone> Zone { get; set; }
        public virtual DbSet<Village> Village { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<LandNotification> LandNotification { get; set; }
        public virtual DbSet<Interest> Interest { get; set; }
        public virtual DbSet<PropertyType> PropertyType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Reasons> Reasons { get; set; }
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
        public virtual DbSet<Khasra> Khasra { get; set; }
        public virtual DbSet<LandCategory> LandCategory { get; set; }
        public virtual DbSet<Scheme> Scheme { get; set; }
        public virtual DbSet<Proposaldetails> Proposaldetails { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }

        public virtual DbSet<Undersection4> Undersection4 { get; set; }
        public virtual DbSet<Undersection4plot> Undersection4plot { get; set; }

        public virtual DbSet<Proposalplotdetails> Proposalplotdetails { get; set; }
        public virtual DbSet<Undersection17> Undersection17 { get; set; }
        public virtual DbSet<Undersection22> Undersection22 { get; set; }
        public virtual DbSet<Awardmasterdetail> Awardmasterdetail { get; set; }
        public virtual DbSet<Awardplotdetails> Awardplotdetails { get; set; }
        public virtual DbSet<Disposallandtype> Disposallandtype { get; set; }
        public virtual DbSet<Nazul> Nazul { get; set; }
        public virtual DbSet<Jointsurvey> Jointsurvey { get; set; }
        public virtual DbSet<Enhancecompensation> Enhancecompensation { get; set; }
        public virtual DbSet<Utilizationtype> Utilizationtype { get; set; }
        public virtual DbSet<Disposalland> Disposalland { get; set; }
        public virtual DbSet<Serialnumber> Serialnumber { get; set; }
        public virtual DbSet<Morland> Morland { get; set; }
        public virtual DbSet<Enchroachment> Enchroachment { get; set; }

        public virtual DbSet<Ldoland> Ldoland { get; set; }
        public virtual DbSet<Booktransferland> Booktransferland { get; set; }
        public virtual DbSet<Deletedproperty> Deletedproperty { get; set; }
        public virtual DbSet<Restoreproperty> Restoreproperty { get; set; }
        public virtual DbSet<AssignPageRoleWise> AssignPageRoleWises { get; set; }
        public virtual DbSet<Khewat> Khewat { get; set; }
        public virtual DbSet<Sakanidetail> Sakanidetail { get; set; }
        public virtual DbSet<Khatauni> Khatauni { get; set; }
        public virtual DbSet<Taraf> Taraf { get; set; }
        public virtual DbSet<Jaraidetail> Jaraidetail { get; set; }
        public virtual DbSet<Undersection6> Undersection6 { get; set; }

        public virtual DbSet<WorkflowTemplate> WorkflowTemplate { get; set; }
        public virtual DbSet<Landtransfer> Landtransfer { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        /// <summary>
        // Encroachment demolition module:
        public virtual DbSet<Watchandwardphotofiledetails> Watchandwardphotofiledetails { get; set; }
        public virtual DbSet<Watchandwardreportfiledetails> Watchandwardreportfiledetails { get; set; }
        public virtual DbSet<Watchandward> Watchandward { get; set; }
        public virtual DbSet<Onlinecomplaint> Onlinecomplaint { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ComplaintType> ComplaintType { get; set; }
        public virtual DbSet<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public virtual DbSet<DetailsOfEncroachment> DetailsOfEncroachment { get; set; }
        public virtual DbSet<EncroachmentFirFileDetails> EncroachmentFirFileDetails { get; set; }
        public virtual DbSet<EncroachmentPhotoFileDetails> EncroachmentPhotoFileDetails { get; set; }
        public virtual DbSet<EncroachmentLocationMapFileDetails> EncroachmentLocationMapFileDetails { get; set; }

        /// </summary>
        /// <param name="modelBuilder"></param>
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
            modelBuilder.ApplyConfiguration(new SchemeConfiguration());
            modelBuilder.ApplyConfiguration(new ProposaldetailsConfiguration());
            modelBuilder.ApplyConfiguration(new Undersection4Configuration());
            modelBuilder.ApplyConfiguration(new Undersection4plotConfiguration());
            modelBuilder.ApplyConfiguration(new Undersection17Configuration());
            modelBuilder.ApplyConfiguration(new ProposalplotdetailsConfiguration());
            modelBuilder.ApplyConfiguration(new Undersection22Configuration());
            modelBuilder.ApplyConfiguration(new AwardplotDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DisposallandtypeConfiguration());
            modelBuilder.ApplyConfiguration(new JointsurveyConfiguration());
            modelBuilder.ApplyConfiguration(new EnhancecompensationConfiguration());
            modelBuilder.ApplyConfiguration(new UtilizationtypeConfiguration());
            modelBuilder.ApplyConfiguration(new DisposallandConfiguration());
            modelBuilder.ApplyConfiguration(new ReasonsConfiguration());
            modelBuilder.ApplyConfiguration(new SerialnumberConfiguration());
            modelBuilder.ApplyConfiguration(new NatureofencroachmentConfiguration());
            modelBuilder.ApplyConfiguration(new EnchroachmentConfiguration());
            modelBuilder.ApplyConfiguration(new LdolandConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new BooktransferlandConfiguration());
            modelBuilder.ApplyConfiguration(new DeletedPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new RestorepropertyConfiguration());
            modelBuilder.ApplyConfiguration(new AssignPageRoleWiseConfiguration());
            modelBuilder.ApplyConfiguration(new SakanidetailConfiguration());
            modelBuilder.ApplyConfiguration(new JaraidetailConfiguration());
            modelBuilder.ApplyConfiguration(new WorkflowTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new LandtransferConfiguration());
            modelBuilder.ApplyConfiguration(new ActionsConfiguration());
            modelBuilder.ApplyConfiguration(new WatchandwardConfiguration());
            modelBuilder.ApplyConfiguration(new OnlinecomplaintConfiguration());
            modelBuilder.ApplyConfiguration(new EncroachmentRegisterationConfiguration());
            modelBuilder.ApplyConfiguration(new EncroachmentFirFileDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new EncroachmentLocationMapFileDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new EncroachmentPhotoFileDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new WatchandwardphotofiledetailsConfiguration());
            modelBuilder.ApplyConfiguration(new WatchandwardreportfiledetailsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}