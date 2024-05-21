using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using LandInventory.Helper;
using Repository.IEntityRepository;
using Repository.EntityRepository;
using Service.ApplicationService;
using Service.IApplicationService;

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
            services.AddScoped<IStatusofVacantLandRepository, StatusofVacantLandRepository>();
            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            services.AddScoped<ILandAcquisitionAwardsRepository, LandAcquisitionAwardsRepository>();
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
            services.AddScoped<IUserWiseLandStatusReportRepository, UserWiseLandStatusReportRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IApplicationModificationDetailsRepository, ApplicationModificationDetailsRepository>();


            /* Application Services */

            services.AddScoped<IApplicationModificationDetailsService, ApplicationModificationDetailsService>();
            services.AddScoped<ILandAcquisitionAwardsService, LandAcquisitionAwardsService>();
            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IStatusofVacantLandService, StatusofVacantLandService>();
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
            services.AddScoped<IUserWiseLandStatusReportService, UserWiseLandStatusReportService>();
            services.AddScoped<IAuditService, AuditService>();
        }
    }
}