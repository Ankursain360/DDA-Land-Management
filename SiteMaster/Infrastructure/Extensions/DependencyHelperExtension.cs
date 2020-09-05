using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;

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
            services.AddScoped<IModuleRepository, ModuleRepository>();

            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IZoneService, ZoneService>();

            services.AddScoped<IModuleService, ModuleService>();

        }
    }
}