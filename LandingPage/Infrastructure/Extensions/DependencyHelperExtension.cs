using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;

namespace LandingPage.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
          
            services.AddScoped<IModuleRepository, ModuleRepository>();
          


            /* Application Services */
         
            services.AddScoped<IModuleService, ModuleService>();
                  }
    }
}