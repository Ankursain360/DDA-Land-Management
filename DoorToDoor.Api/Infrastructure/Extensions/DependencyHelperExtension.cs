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
using DoorToDoor.Api.Helper;

namespace DoorToDoor.Api.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          //  services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */
            services.AddScoped<IDoor2DoorAPIRepository, Door2DoorAPIRepository>();


            /* Application Services */
            services.AddScoped<IDoor2DoorAPIService, Door2DoorAPIService>();

        }
    }
}