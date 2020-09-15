using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;

namespace AcquiredLandInformationManagement.Infrastructure.Extensions
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

            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            services.AddScoped<ISchemeRepository, SchemeRepository>();

            services.AddScoped<IUndersection4Repository, Undersection4Repository>();




            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<IUndersection4service, Undersection4Service>();






        }
    }
}