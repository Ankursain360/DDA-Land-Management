using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using LandingPage.Helper;
using Repository.IEntityRepository;
using Repository.EntityRepository;
using Service.IApplicationService;
using Service.ApplicationService;

namespace LandingPage.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();
            /* Respository */

            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IModuleCategoryRepository, ModuleCategoryRespository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IApplicationModificationDetailsRepository, ApplicationModificationDetailsRepository>();

            /* Application Services */

            services.AddScoped<IApplicationModificationDetailsService, ApplicationModificationDetailsService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IModuleCategoryService, ModuleCategoryService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();

        }
    }
}