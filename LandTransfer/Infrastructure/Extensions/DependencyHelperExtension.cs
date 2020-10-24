using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<ILandTransferRepository, LandtransferRepository>();
            services.AddScoped<ICurrentstatusoflandhistoryRepository, CurrentstatusoflandhistoryRepository>();
            /* Application Services */
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<ILandTransferService, LandTransferService>();
            services.AddScoped<ICurrentstatusoflandhistoryService, CurrentstatusoflandhistoryService>();
        }

    }
}