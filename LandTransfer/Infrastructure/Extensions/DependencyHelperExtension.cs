using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;

namespace LandTransfer.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
		{
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
            services.AddScoped<INazullandRepository, NazullandRepository>();
            //services.AddScoped<ILandTransferRepository, LandtransferRepository>();

            /* Application Services */
            services.AddScoped<INazullandService, NazullandService>();
            //services.AddScoped<ILandTransferService, LandTransferService>();
        }
    }
}