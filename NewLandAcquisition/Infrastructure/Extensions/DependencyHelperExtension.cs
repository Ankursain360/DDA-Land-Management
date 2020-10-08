using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;

namespace NewLandAcquisition.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();




            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();



        }
    }
}