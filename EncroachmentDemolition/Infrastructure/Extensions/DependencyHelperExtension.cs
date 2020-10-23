using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace EncroachmentDemolition.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IWatchandwardRepository, WatchandwardRepository>();
            services.AddScoped<IEncroachmentRegisterationRepository, EncroachmentRegisterationRepository>();
            services.AddScoped<IOnlinecomplaintRepository, OnlinecomplaintRepository>();
            services.AddScoped<IWatchAndWardApprovalRepository, WatchAndWardApprovalRepository>();
            services.AddScoped<IAnnexureARepository, AnnexureARepository>();


            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IWatchandwardService,WatchandwardService>();
            services.AddScoped<IEncroachmentRegisterationService, EncroachmentRegisterationService>();
            services.AddScoped<IOnlinecomplaintService, OnlinecomplaintService>();
            services.AddScoped<IWatchAndWardApprovalService, WatchAndWardApprovalService>();
            services.AddScoped<IAnnexureAService, AnnexureAService>();

        }
    }
}