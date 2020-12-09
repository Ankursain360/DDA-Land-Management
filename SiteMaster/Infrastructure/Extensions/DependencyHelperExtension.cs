using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Repository.IEntityRepository;
using Repository.EntityRepository;
using Service.IApplicationService;
using Service.ApplicationService;
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
        }
    }
}