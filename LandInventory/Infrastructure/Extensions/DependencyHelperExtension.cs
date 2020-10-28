using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;
using LandInventory.Helper;

namespace LandInventory.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
		{
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */
            services.AddScoped<INazullandRepository, NazullandRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();

            /* Application Services */
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
        }
    }
}