using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Repository.IEntityRepository;
using Repository.EntityRepository;
using Service.IApplicationService;
using Service.ApplicationService;
using LandInventory.Helper;

namespace LandInventory.Infrastructure.Extensions
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
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            /*Land transfer */
            services.AddScoped<INazullandRepository, NazullandRepository>();
            services.AddScoped<ILandTransferRepository, LandtransferRepository>();
            services.AddScoped<ICurrentstatusoflandhistoryRepository, CurrentstatusoflandhistoryRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            services.AddScoped<IPlanningRepositry, PlanningRepositry>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            /* Application Services */
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            /* Land transfer Services */
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<ILandTransferService, LandTransferService>();
            services.AddScoped<ICurrentstatusoflandhistoryService, CurrentstatusoflandhistoryService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IPlanningService, PlanningService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IModuleService, ModuleService>();
        }
    }
}