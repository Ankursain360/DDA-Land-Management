using LandTransfer.Helper;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.Extensions.DependencyInjection;
using Repository.EntityRepository;
using Repository.IEntityRepository;
using Service.ApplicationService;
using Service.IApplicationService;

namespace LandTransfer.Infrastructure.Extensions
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
            services.AddScoped<ILandTransferRepository, LandtransferRepository>();
            services.AddScoped<ICurrentstatusoflandhistoryRepository, CurrentstatusoflandhistoryRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            /* Application Services */
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<ILandTransferService, LandTransferService>();
            services.AddScoped<ICurrentstatusoflandhistoryService, CurrentstatusoflandhistoryService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}