using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.Extensions.DependencyInjection;
using Repository.EntityRepository;
using Repository.IEntityRepository;
using Service.ApplicationService;
using Service.IApplicationService;
using SiteMaster.Helper;

namespace SiteMaster.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();
            /********** Respository ***********/
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<ILocalityRepository, LocalityRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRebateRepository, RebateRepository>();
            services.AddScoped<ILandUseRepository, LandUseRepository>();
            services.AddScoped<IClassificationOfLandRepository, ClassificationOfLandRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IDemolitionchecklistRepository, DemolitionchecklistRepository>();
            services.AddScoped<IDemolitiondocumentRepository, DemolitiondocumentRepository>();
            services.AddScoped<IDemolitionprogrammasterRepository, DemolitionprogrammasterRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IStructureRepository, StructureRepository>();
            services.AddScoped<ICaseyearRepository, CaseyearRepository>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IApprovalstatusRepository, ApprovalstatusRepository>();
            services.AddScoped<ISchemeFileLoadingRepository, SchemeFileLoadingRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IDepartmenttargetRepository, DepartmenttargetRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IApprovalCompleteRepository, ApprovalCompleteRepository>();
            services.AddScoped<IApplicationNotificationTemplateRepository, ApplicationNotificationTemplateRepository>();
            services.AddScoped<IModuleCategoryRepository,ModuleCategoryRespository>();
            services.AddScoped<IAlmirahRepository, AlmirahRespository>();
            services.AddScoped<IBundleRepository, BundleRespository>();
            // new land acquisition masters

            services.AddScoped<INewlandvillageRepository, NewlandVillageRepository>();
            services.AddScoped<INewlandkhasraRepository, NewlandkhasraRepository>();
            services.AddScoped<INewlandSchemeRepository, NewlandSchemeRepository>();
            services.AddScoped<INewlandProposaldetailsRepository, NewlandProposaldetailsRepository>();
            services.AddScoped<INewlandawardmasterdetailRepository, NewlandawardmasterdetailRepository>();

            // Acquired land  masters


            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            services.AddScoped<IKhasraRepository, KhasraRepository>();
            services.AddScoped<ITehsilRepository, TehsilRepository>();//added by anuj 8-feb-21
            services.AddScoped<IProposaldetailsRepository, ProposaldetailsRepository>();
            services.AddScoped<IUndersection4Repository, Undersection4Repository>();
            services.AddScoped<IUnderSection6Repository, UnderSection6Repository>();
            services.AddScoped<IUndersection17Repository, Undersection17Repository>();
            services.AddScoped<IUndersection22Repository, Undersection22Repository>();
            services.AddScoped<IAwardmasterdetailsRepository, AwardmasterdetailsRepository>();

            //Lease masters 


            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<IGroundRentRepository, GroundRentRepository>();
            services.AddScoped<IPremiumrateRepository, PremiumrateRepository>();
            services.AddScoped<IDocumentchargesRepository, DocumentchargesRepository>();
            services.AddScoped<ILicenceFeesRepository, LicenceFeesRepository>();
            services.AddScoped<IInterestrateRepository, InterestrateRepository>();
            services.AddScoped<IDocumentCheckListRepository, DocumentCheckListRepository>(); services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<ILeasepurposeRepository, LeasepurposeRepository>();

            services.AddScoped<ILeasesubpurposeRepository, LeasesubpurposeRepository>();
            services.AddScoped<IJudgementstatusRepository, JudgementstatusRepository>();
            services.AddScoped<IHonbleRepository, HonbleRepository>();
            services.AddScoped<ILeasePaymentTypeRepository, LeasePaymentTypeRepository>();

            /************ Application Services ************/
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<ILocalityService, LocalityService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IRebateService, RebateService>();
            services.AddScoped<ILandUseService, LandUseService>();
            services.AddScoped<IClassificationOfLandService, ClassificationOfLandService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IDemolitionchecklistService, DemolitionchecklistService>();
            services.AddScoped<IDemolitiondocumentService, DemolitiondocumentService>();
            services.AddScoped<IDemolitionprogrammasterService, DemolitionprogrammasterService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IStructureService, StructureService>();
            services.AddScoped<ICaseyearService, CaseyearService>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<IApprovalstatusService, ApprovalstatusService>();
            services.AddScoped<ISchemeFileLoadingService, SchemeFileLoadingService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDepartmenttargetService, DepartmenttargetService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IApprovalCompleteService, ApprovalCompleteService>();
            services.AddScoped<IApplicationNotificationTemplateService, ApplicationNotificationTemplateService>();
            services.AddScoped<IModuleCategoryService, ModuleCategoryService>();
            services.AddScoped<IAlmirahService, AlmirahService>();
            services.AddScoped<IBundleService, BundleService>();
            // new land acquisition masters 

            services.AddScoped<INewlandvillageService, NewlandvillageService>();
            services.AddScoped<INewlandkhasraService, NewlandkhasraService>();
            services.AddScoped<INewlandSchemeService, NewlandSchemeService>();
            services.AddScoped<INewlandProposaldetailsService, NewlandProposaldetailsService>();
            services.AddScoped<INewlandawardmasterdetailService, NewlandawardmasterdetailsService>();


            // Acquired land  masters


            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<IKhasraService, KhasraService>();
            services.AddScoped<ITehsilService, TehsilService>();//added by anuj 8-feb-21
            services.AddScoped<IProposaldetailsService, ProposaldetailsService>();
            services.AddScoped<IUndersection4service, Undersection4Service>();
            services.AddScoped<IUnderSection6Service, UnderSection6Service>();
            services.AddScoped<IUndersection17Service, Undersection17Service>();
            services.AddScoped<IUndersection22Service, Undersection22Service>();
            services.AddScoped<IAwardmasterdetailsService, AwardmasterdetailsService>();

            // Lease masters

            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<IGroundRentService, GroundRentService>();
            services.AddScoped<IPremiumrateService, PremiumrateService>();
            services.AddScoped<IDocumentchargesServices, DocumentchargesServices>();
            services.AddScoped<ILicenceFeesService, LicenceFeesService>();
            services.AddScoped<IInterestrateService, InterestrateService>();

            services.AddScoped<IDocumentCheckListService, DocumentCheckListService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();

            services.AddScoped<ILeasepurposeService, LeasepurposeService>();
            services.AddScoped<ILeasesubpurposeService, LeasesubpurposeService>();
            services.AddScoped<IJudgementstatusService, JudgementstatusService>();
            services.AddScoped<IHonbleService, HonbleService>();
            services.AddScoped<ILeasePaymentTypeService, LeasePaymentTypeService>();

        }
    }
}
