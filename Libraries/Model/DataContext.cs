using Libraries.Model.builderConfiguration;
using Libraries.Model.Entity;
using Libraries.Model.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Model.EntityConfiguration;

namespace Libraries.Model
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<Approvalstatus> Approvalstatus { get; set; }
        public virtual DbSet<Userprofile> Userprofile { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Natureofencroachment> Natureofencroachment { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }
        public virtual DbSet<Village> Village { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<LandNotification> LandNotification { get; set; }
        public virtual DbSet<Interest> Interest { get; set; }
        public virtual DbSet<PropertyType> PropertyType { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Reasons> Reasons { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Rebate> Rebate { get; set; }
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
        public virtual DbSet<Khewat> Khewat { get; set; }
        public virtual DbSet<Sakanidetail> Sakanidetail { get; set; }
        public virtual DbSet<Khatauni> Khatauni { get; set; }
        public virtual DbSet<Taraf> Taraf { get; set; }
        public virtual DbSet<Jaraidetail> Jaraidetail { get; set; }
        public virtual DbSet<Undersection6> Undersection6 { get; set; }
        public virtual DbSet<WorkflowTemplate> WorkflowTemplate { get; set; }
        public virtual DbSet<Landtransfer> Landtransfer { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Currentstatusoflandhistory> Currentstatusoflandhistory { get; set; }
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
        public virtual DbSet<Demolitionstructure> Demolitionstructure { get; set; }
        public virtual DbSet<Demolitionstructureafterdemolitionphotofiledetails> Demolitionstructureafterdemolitionphotofiledetails { get; set; }
        public virtual DbSet<Demolitionstructurebeforedemolitionphotofiledetails> Demolitionstructurebeforedemolitionphotofiledetails { get; set; }
        public virtual DbSet<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
        public virtual DbSet<Demolitionchecklist> Demolitionchecklist { get; set; }
        public virtual DbSet<Demolitiondocument> Demolitiondocument { get; set; }
        public virtual DbSet<Demolitionprogram> Demolitionprogram { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<Fixingdemolition> Fixingdemolition { get; set; }
        public virtual DbSet<Fixingchecklist> Fixingchecklist { get; set; }
        public virtual DbSet<Fixingprogram> Fixingprogram { get; set; }
        public virtual DbSet<Fixingdocument> Fixingdocument { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Disposedproperty> Disposedproperty { get; set; }
        public virtual DbSet<PropertyRegistrationHistory> Propertyregistrationhistory { get; set; }
        public virtual DbSet<Menuactionrolemap> Menuactionrolemap { get; set; }
        public virtual DbSet<PlanningProperties> PlanningProperties { get; set; }
        public virtual DbSet<Planning> Planning { get; set; }
        public virtual DbSet<AssignedPropertyDailyRoaster> Assignedpropertydailyroaster { get; set; }
        public virtual DbSet<DailyRoaster> DailyRoaster { get; set; }
        public virtual DbSet<MonthlyRoaster> MonthlyRoaster { get; set; }
        public virtual DbSet<Approvalproccess> Approvalproccess { get; set; }
        public virtual DbSet<Payeeregistration> Payeeregistration { get; set; }
        public virtual DbSet<Familydetails> Familydetails { get; set; }
        public virtual DbSet<Lawyer> Lawyer { get; set; }
        public virtual DbSet<Demandletters> Demandletters { get; set; }
        public virtual DbSet<Schemefileloading> Schemefileloading { get; set; }
        public virtual DbSet<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public virtual DbSet<Departmenttarget> Departmenttarget { get; set; }

        //**********  Court case management**********
        public virtual DbSet<Legalmanagementsystem> Legalmanagementsystem { get; set; }
        public virtual DbSet<Court> Court { get; set; }
        public virtual DbSet<Caseyear> Caseyear { get; set; }
        public virtual DbSet<Courttype> Courttype { get; set; }
        public virtual DbSet<Casestatus> Casestatus { get; set; }
        public virtual DbSet<Demolitionpoliceassistenceletter> Demolitionpoliceassistenceletter { get; set; }
        public virtual DbSet<Casenature> Casenature { get; set; }
        //**********  File Loading **********
        public virtual DbSet<Almirah> Almirah { get; set; }
        public virtual DbSet<Row> Row { get; set; }
        public virtual DbSet<Column> Column { get; set; }

        public virtual DbSet<Bundle> Bundle { get; set; }

        public virtual DbSet<Issuereturnfile> Issuereturnfile { get; set; }

        public virtual DbSet<Datastoragedetails> Datastoragedetails { get; set; }

        public virtual DbSet<Datastoragepartfilenodetails> Datastoragepartfilenodetails { get; set; }

        //**********  Damage Payee **********

        public virtual DbSet<Allottetype> Allottetype { get; set; }

        public virtual DbSet<Damagepayeepersonelinfo> Damagepayeepersonelinfo { get; set; }
        public virtual DbSet<Damagepayeeregister> Damagepayeeregister { get; set; }
        public virtual DbSet<Damagepaymenthistory> Damagepaymenthistory { get; set; }

        public virtual DbSet<Paymentverification> Paymentverification { get; set; }
        public virtual DbSet<Mutationolddamageassesse> Mutationolddamageassesse { get; set; }
        public virtual DbSet<Mutationnewdamageassesse> Mutationnewdamageassesse { get; set; }
        public virtual DbSet<Mutationdetails> Mutationdetails { get; set; }
        public virtual DbSet<Mutationdetailsphotoproperty> Mutationdetailsphotoproperty { get; set; }
        public virtual DbSet<Mutationnewowner> Mutationnewowner { get; set; }
        public virtual DbSet<Mutationnewownertemp> Mutationnewownertemp { get; set; }
        public virtual DbSet<Mutationoriginalowner> Mutationoriginalowner { get; set; }
        public virtual DbSet<Mutationoriginalownertemp> Mutationoriginalownertemp { get; set; }

        public virtual DbSet<Noticetodamagepayee> Noticetodamagepayee { get; set; }
        public virtual DbSet<Presentuse> Presentuse { get; set; }
        public virtual DbSet<Doortodoorsurvey> Doortodoorsurvey { get; set; }
        public virtual DbSet<Processworkflow> Processworkflow { get; set; }
        public virtual DbSet<Workflowaction> Workflowaction { get; set; }
        public virtual DbSet<Encrochmenttype> Encrochmenttype { get; set; }
        public virtual DbSet<Resratelisttypea> Resratelisttypea { get; set; }
        public virtual DbSet<Resratelisttypeb> Resratelisttypeb { get; set; }
        public virtual DbSet<Resratelisttypec> Resratelisttypec { get; set; }
        public virtual DbSet<Demandletter> Demandletter { get; set; }
        public virtual DbSet<Comratelisttypea> Comratelisttypea { get; set; }
        public virtual DbSet<Comratelisttypeb> Comratelisttypeb { get; set; }
        public virtual DbSet<Comratelisttypec> Comratelisttypec { get; set; }
        public virtual DbSet<Comsubencroacherstype> Comsubencroacherstype { get; set; }
        public virtual DbSet<Ressubencroacherstype> Ressubencroacherstype { get; set; }
        public virtual DbSet<Comencrochmenttype> Comencrochmenttype { get; set; }
        public virtual DbSet<Mutationdetailstemp> Mutationdetailstemp { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Dmsfileupload> Dmsfileupload { get; set; }
        public virtual DbSet<Plot> Plot { get; set; }
        public virtual DbSet<Dmsfileright> Dmsfileright { get; set; }

        public virtual DbSet<Gisaabadi> Gisaabadi { get; set; }
        public virtual DbSet<Gisburji> Gisburji { get; set; }
        public virtual DbSet<GISClean> Gisclean { get; set; }
        public virtual DbSet<GisCleanText> Giscleantext { get; set; }
        public virtual DbSet<Gisdim> Gisdim { get; set; }
        public virtual DbSet<GISEncroachment> Gisencroachment { get; set; }
        public virtual DbSet<Gisgosha> Gisgosha { get; set; }
        public virtual DbSet<Gisgrid> Gisgrid { get; set; }
        public virtual DbSet<GISEncroachment> Gisnala { get; set; }
        public virtual DbSet<Gistext> Gistext { get; set; }
        public virtual DbSet<Gistrijunction> Gistrijunction { get; set; }
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
            modelBuilder.ApplyConfiguration(new InterestConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocalityConfiguration());
            modelBuilder.ApplyConfiguration(new RateConfiguration());
            modelBuilder.ApplyConfiguration(new RebateConfiguration());
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
            modelBuilder.ApplyConfiguration(new DemolitionstructureafterdemolitionphotofiledetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitionstructurebeforedemolitionphotofiledetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitionstructureConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitionstructuredetailsConfiguration());
            modelBuilder.ApplyConfiguration(new CurrentstatusoflandhistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitionchecklistConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitiondocumentConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new DemolitionprogrammasterConfiguration());
            modelBuilder.ApplyConfiguration(new FixingdemolitionConfiguration());
            modelBuilder.ApplyConfiguration(new FixingchecklistConfiguration());
            modelBuilder.ApplyConfiguration(new FixingprogramConfiguration());
            modelBuilder.ApplyConfiguration(new FixingdocumentConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new DisposedPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyRegistrationHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new MenuactionrolemapConfiguration());
            modelBuilder.ApplyConfiguration(new PlanningConfiguration());
            modelBuilder.ApplyConfiguration(new PlanningPropertiesConfiguration());
            modelBuilder.ApplyConfiguration(new AssignedPropertyDailyRoasterConfiguration());
            modelBuilder.ApplyConfiguration(new MonthlyRosterConfiguration());
            modelBuilder.ApplyConfiguration(new DailyRoasterConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovalProccessConfiguration());
            modelBuilder.ApplyConfiguration(new StructureConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovalstatusConfiguration());
            modelBuilder.ApplyConfiguration(new CasenatureConfiguration());

            modelBuilder.ApplyConfiguration(new LegalmanagementsystemConfiguration());
            modelBuilder.ApplyConfiguration(new CourtConfiguration());
            modelBuilder.ApplyConfiguration(new CaseyearConfiguration());
            modelBuilder.ApplyConfiguration(new CourttypeConfiguration());
            modelBuilder.ApplyConfiguration(new CasestatusConfiguration());
            modelBuilder.ApplyConfiguration(new Undersection22plotdetailsConfiguration());

            //************* Data Loading **********************
            modelBuilder.ApplyConfiguration(new DatastoragepartfilenodetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DataStorageConfiguration());
            modelBuilder.ApplyConfiguration(new AlmirahConfiguration());
            modelBuilder.ApplyConfiguration(new RowConfiguration());
            modelBuilder.ApplyConfiguration(new ColumnConfiguration());
            modelBuilder.ApplyConfiguration(new BundleConfiguration());
            modelBuilder.ApplyConfiguration(new IssuereturnfileConfiguration());

            //modelBuilder.ApplyConfiguration(new PayeeregistrationConfiguration());

            modelBuilder.ApplyConfiguration(new DemolitionPoliceAssistenceLetterConfiguration());
            modelBuilder.ApplyConfiguration(new MutationDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new MutationOldDamageAssesseConfiguration());
            modelBuilder.ApplyConfiguration(new MutationNewDamageAssesseConfiguration());
            modelBuilder.ApplyConfiguration(new MutationDetailsPhotoPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new MutationNewOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new MutationNewOwnerTempConfiguration());
            modelBuilder.ApplyConfiguration(new MutationOriginalOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new MutationOriginalOwnerTempConfiguration());

            //**********  Damage Payee **********

            modelBuilder.ApplyConfiguration(new AllottetypeConfiguration());
            modelBuilder.ApplyConfiguration(new DamagepayeepersonelinfoConfiguration());
            modelBuilder.ApplyConfiguration(new DamagepayeeregisterConfiguration());
            modelBuilder.ApplyConfiguration(new DamagepaymenthistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DoortodoorsurveyConfiguration());
            modelBuilder.ApplyConfiguration(new FamilydetailsConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentverificationConfiguration());
            modelBuilder.ApplyConfiguration(new WorkflowActionConfiguration());
            modelBuilder.ApplyConfiguration(new ProcessWorkflowConfiguration());
            modelBuilder.ApplyConfiguration(new NoticetodamagepayeeConfiguration());
            modelBuilder.ApplyConfiguration(new EncrochmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ResRateListTypeAConfiguration());
            modelBuilder.ApplyConfiguration(new ResRateListTypeBConfiguration());
            modelBuilder.ApplyConfiguration(new ResRateListTypeCConfiguration());
            modelBuilder.ApplyConfiguration(new ResSubEncroachersTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ComRateListTypeAConfiguration());
            modelBuilder.ApplyConfiguration(new ComRateListTypeBConfiguration());
            modelBuilder.ApplyConfiguration(new ComRateListTypeCConfiguration());
            modelBuilder.ApplyConfiguration(new ComSubEncroachersTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ComEncrochmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DemandletterConfiguration());
            modelBuilder.ApplyConfiguration(new MutationDetailsTempConfiguration());
            modelBuilder.ApplyConfiguration(new DemandlettersConfiguration());
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new DmsFileUploadConfiguration());
            modelBuilder.ApplyConfiguration(new DmsfilerightConfiguration());
            //**********  Damage Payee **********
            modelBuilder.ApplyConfiguration(new PlotConfiguration());
            modelBuilder.ApplyConfiguration(new SchemefileloadingConfiguration());
            //**********  GIS **********
            modelBuilder.ApplyConfiguration(new GisaabadiConfiguration());
            modelBuilder.ApplyConfiguration(new GisburjiConfiguration());
            modelBuilder.ApplyConfiguration(new GISCleanConfiguration());
            modelBuilder.ApplyConfiguration(new GisCleanTextConfiguration());
            modelBuilder.ApplyConfiguration(new GisdimConfiguration());
            modelBuilder.ApplyConfiguration(new GISEncroachmentConfiguration());
            modelBuilder.ApplyConfiguration(new GisgoshaConfiguration());
            modelBuilder.ApplyConfiguration(new GisgridConfiguration());
            modelBuilder.ApplyConfiguration(new GisnalaConfiguration());
            modelBuilder.ApplyConfiguration(new GistextConfiguration());
            modelBuilder.ApplyConfiguration(new GistrijunctionConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmenttargetConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
