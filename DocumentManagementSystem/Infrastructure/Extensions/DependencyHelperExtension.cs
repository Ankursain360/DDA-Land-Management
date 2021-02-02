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
using DocumentManagementSystem.Helper;

namespace DocumentManagementSystem.Infrastructure.Extensions
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
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IDmsFileUploadRepository, DmsFileUploadRepository>();


            /* Application Services */
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IDmsFileUploadService, DmsFileUploadService>();

        }
    }
}
