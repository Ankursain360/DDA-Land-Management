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
//using AuthServer.Helper;
namespace AuthServer.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */

           // services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IPasswordhistoryRepository, PasswordhistoryRepository>();
            services.AddScoped<IApplicationModificationDetailsRepository, ApplicationModificationDetailsRepository>();


            /* Application Services */
           // services.AddScoped<IUserProfileService, UserProfileService>();
           services.AddScoped<IApplicationModificationDetailsService, ApplicationModificationDetailsService>();
            services.AddScoped<IPasswordhistoryService, PasswordhistoryService>();

        }
    }
}