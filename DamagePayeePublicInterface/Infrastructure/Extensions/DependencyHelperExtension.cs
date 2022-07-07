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
using DamagePayeePublicInterface.Helper;

namespace DamagePayeePublicInterface.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
		{
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<ISelfAssessmentDamageRepository, SelfAssessmentDamageRepository>();
            services.AddScoped<IMutationDetailsRepository, MutationDetailsRepository>();
            services.AddScoped<IDamagePayeeRegistrationRepository, DamagePayeeRegistrationRepository>();
            services.AddScoped<IDamagepayeeregisterRepository, DamagepayeeregisterRepository>();
            services.AddScoped<IDamageCalculationRepository, DamageCalculationRepository>();
            services.AddScoped<IDPPublicPaymentRepository, DPPublicPaymentRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<INewDamageSelfAssessmentRepository, NewDamageSelfAssessmentRepository>();


            /* Application Services */
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<ISelfAssessmentDamageService, SelfAssessmentDamageService>();
            services.AddScoped<IMutationDetailsService, MutationDetailsService>();
            services.AddScoped<IDamagePayeeRegistrationService, DamagePayeeRegistrationService>();
            services.AddScoped<IDamagepayeeregisterService, DamagepayeeregisterService>();
            services.AddScoped<IDamageCalculationService, DamageCalculationService>();
            services.AddScoped<IDPPublicPaymentService, DPPublicPaymentService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<INewDamageSelfAssessmentService, NewDamageSelfAssessmentService>();

        }
    }
}