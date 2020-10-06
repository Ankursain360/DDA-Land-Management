using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace SiteMaster.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILocalityRepository, LocalityRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRebateRepository, RebateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPageRoleRepository, PageRoleRepository>();
            services.AddScoped<ILandUseRepository, LandUseRepository>();
            services.AddScoped<IClassificationOfLandRepository, ClassificationOfLandRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();



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
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILocalityService, LocalityService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IRebateService, RebateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPageRoleService, PageRoleService>();
            services.AddScoped<ILandUseService, LandUseService>();
            services.AddScoped<IClassificationOfLandService, ClassificationOfLandService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
        }
    }
}