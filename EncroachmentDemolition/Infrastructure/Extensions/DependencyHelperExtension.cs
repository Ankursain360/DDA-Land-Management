using EncroachmentDemolition.Helper;
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

namespace EncroachmentDemolition.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IWatchandwardRepository, WatchandwardRepository>();
            services.AddScoped<IEncroachmentRegisterationRepository, EncroachmentRegisterationRepository>();
            services.AddScoped<IOnlinecomplaintRepository, OnlinecomplaintRepository>();
            services.AddScoped<IWatchAndWardApprovalRepository, WatchAndWardApprovalRepository>();
            services.AddScoped<IAnnexureARepository, AnnexureARepository>();
            services.AddScoped<IDemolitionstructuredetailsRepository, DemolitionStructureDetailsRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMonthlyRosterRepository, MonthlyRosterRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();

            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IWatchandwardService, WatchandwardService>();
            services.AddScoped<IEncroachmentRegisterationService, EncroachmentRegisterationService>();
            services.AddScoped<IOnlinecomplaintService, OnlinecomplaintService>();
            services.AddScoped<IWatchAndWardApprovalService, WatchAndWardApprovalService>();
            services.AddScoped<IAnnexureAService, AnnexureAService>();
            services.AddScoped<IDemolitionstructuredetailsService, DemolitionstructuredetailsService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IMonthlyRosterService, MonthlyRosterService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IModuleService, ModuleService>();
        }
    }
}
