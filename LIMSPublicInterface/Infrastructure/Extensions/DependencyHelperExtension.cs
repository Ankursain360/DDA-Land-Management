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
using LIMSPublicInterface.Helper;
namespace LIMSPublicInterface.Infrastructure.Extensions
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
            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            services.AddScoped<INazulRepository, NazulRepository>();
            services.AddScoped<IKhasraRepository, KhasraRepository>();
            services.AddScoped<IAwardplotDetailsRepository, AwardplotDetailsRepository>();
            services.AddScoped<IPossessiondetailsRepository, PossessiondetailsRepository>();
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();
            services.AddScoped<IUndersection6plotRepository, Undersection6plotRepository>();
            services.AddScoped<IUndersection17plotdetailRepository, Undersection17plotdetailRepository>();
            services.AddScoped<IUndersection22plotdetailsRepository, Undersection22plotdetailsRepository>();
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
            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<IAwardplotDetailService, AwardplotDetailsService>();
            services.AddScoped<IPossessiondetailsService, PossessiondetailsService>();
            services.AddScoped<INazulService, NazulService>();
            services.AddScoped<IKhasraService, KhasraService>();
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();
            services.AddScoped<IUndersection6plotService, Undersection6plotService>();
            services.AddScoped<IUndersection17plotdetailService, Undersection17plotdetailService>();
            services.AddScoped<IUndersection22plotdetailsService, Undersection22plotdetailsService>();
        }
    }
}