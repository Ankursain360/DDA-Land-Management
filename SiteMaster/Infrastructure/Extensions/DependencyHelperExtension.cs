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
            /* Respository */
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
            services.AddScoped<INewlandnotificationdetailsRepository, NewlandnotificationdetailsRepository>();


            /* Application Services */
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
            services.AddScoped<INewlandnotificationdetailsService, NewlandnotificationdetailsService>();
        }
    }
}
