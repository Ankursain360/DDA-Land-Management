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
using NewLandAcquisition.Helper;
namespace NewLandAcquisition.Infrastructure.Extensions
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
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<INewlandProposaldetailsRepository, NewlandProposaldetailsRepository>();
            services.AddScoped<INewlandvillageRepository, NewlandVillageRepository>();
            services.AddScoped<INewlandSchemeRepository, NewlandSchemeRepository>();
            services.AddScoped<INewlandus4plotRepository, Newlandus4plotRepository>();

            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<INewlandProposaldetailsService, NewlandProposaldetailsService>();
            services.AddScoped<INewlandvillageService, NewlandvillageService>();
            services.AddScoped<INewlandSchemeService, NewlandSchemeService>();
            services.AddScoped<INewlandus4plotService, Newlandus4plotService>();

        }
    }
}