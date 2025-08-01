﻿using DamagePayee.Helper;

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

namespace DamagePayee.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */

            services.AddScoped<INewDamageSelfAssessmentRepository, NewDamageSelfAssessmentRepository>();

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
            services.AddScoped<IEncroachmentRegisterationApprovalRepository, EncroachmentRegisterationApprovalRepository>();
            services.AddScoped<IDamagePayeeRegistrationRepository, DamagePayeeRegistrationRepository>();
            services.AddScoped<IDamagepayeeregisterRepository, DamagepayeeregisterRepository>();
            services.AddScoped<IMutationDetailsRepository, MutationDetailsRepository>();
            services.AddScoped<IDoortodoorsurveyRepository, DoortodoorsurveyRepository>();
            services.AddScoped<ISelfAssessmentDamageRepository, SelfAssessmentDamageRepository>();
            services.AddScoped<INoticeToDamagePayeeRepository, NoticeToDamagePayeeRepository>();
            services.AddScoped<IDamagePayeeApprovalRepository,DamagePayeeApprovalRepository>();
            services.AddScoped<IProccessWorkflowRepository, ProccessWorkflowRepository>();
            services.AddScoped<IDamageCalculationRepository, DamageCalculationRepository>();
            services.AddScoped<IDemandLetterRepository, DemandLetterRepository>();
            services.AddScoped<IMutationDetailsRepository, MutationDetailsRepository>();
            services.AddScoped<IPaymentverificationRepository, PaymentverificationRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IApplicationModificationDetailsRepository, ApplicationModificationDetailsRepository>();

            /* Application Services */

            services.AddScoped<IApplicationModificationDetailsService, ApplicationModificationDetailsService>();
            services.AddScoped<INewDamageSelfAssessmentService, NewDamageSelfAssessmentService>();

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
            services.AddScoped<IEncroachmentRegisterationApprovalService, EncroachmentRegisterationApprovalService>();
            services.AddScoped<IDamagePayeeRegistrationService, DamagePayeeRegistrationService>();
            services.AddScoped<IDamagepayeeregisterService, DamagepayeeregisterService>();
            services.AddScoped<IMutationDetailsService, MutationDetailsService>();
            services.AddScoped<IDoortodoorsurveyService, DoortodoorsurveyService>();
            services.AddScoped<ISelfAssessmentDamageService, SelfAssessmentDamageService>();
            services.AddScoped<INoticeToDamagePayeeService, NoticeToDamagePayeeService>();
            services.AddScoped<IDamagePayeeApprovalService, DamagePayeeApprovalService>();
            services.AddScoped<IProccessWorkflowService, ProccessWorkflowService>();
            services.AddScoped<IDamageCalculationService, DamageCalculationService>();
            services.AddScoped<IDemandLetterService, DemandLetterService>();
            services.AddScoped<IMutationDetailsService, MutationDetailsService>();
            services.AddScoped<IPaymentverificationService, PaymentverificationService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IAuditService, AuditService>();
        }
    }
}
