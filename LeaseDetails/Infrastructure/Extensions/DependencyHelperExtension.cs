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
using LeaseDetails.Helper;
namespace LeaseDetails.Infrastructure.Extensions
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
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
           
            
        
            services.AddScoped<ILeaseApplicationFormRepository, LeaseApplicationFormRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
           
            services.AddScoped<ILeaseApplicationFormApprovalRepository, LeaseApplicationFormApprovalRepository>();
            services.AddScoped<IAllotmentEntryRepository, AllotmentEntryRepository>();
            services.AddScoped<ICalculationSheetRepository, CalculationSheetRepository>();
            services.AddScoped<IPossesionplanRepository, PossesionplanRepository>();
            services.AddScoped<IProceedingEvictionLetterRepository, ProceedingEvictionLetterRepository>();
            services.AddScoped<IOldAllotmentEntryRepository, OldAllotmentEntryRepository>();
            services.AddScoped<IRequestforproceedingRepository, RequestforproceedingRepository>();
            services.AddScoped<ILeasepaymentdetailsRepository, LeasepaymentdetailsRepository>();
            services.AddScoped<ILeaseHearingDetailsRepository, LeaseHearingDetailsRepository>();
            services.AddScoped<INoticeGenerationRepository, NoticeGenerationRepository>();
            services.AddScoped<IHearingdetailsRepository, HearingdetailsRepository>();
            services.AddScoped<IJudgementRepository, JudgementRepository>();
            services.AddScoped<IAllotteeEvidenceUploadRepository, AllotteeEvidenceUploadRepository>();
            
            services.AddScoped<IApplyForServicesRepository, ApplyForServicesRepository>();
          
            services.AddScoped<ICancellationEntryRepository, CancellationEntryRepository>();
            services.AddScoped<ILeasedeedRepository, LeasedeedRepository>();
            services.AddScoped<IActionTakenByDdaRepository, ActionTakenByDdaRepository>();
           
            
            services.AddScoped<ITimeextensionRepository, TimeextensionRepository>();
           
            services.AddScoped<IAllotmentLetterRepository, AllotmentLetterRepository>();
            services.AddScoped<IExtensionApprovalRepository, ExtensionApprovalRepository>();
            services.AddScoped<IExtensionRepository, ExtensionRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();

            services.AddScoped<IpaymentTransactionRepository, PaymentTransactionRepository>();

            services.AddScoped<IKycformApprovalRepository, KycformApprovalRepository>();
            services.AddScoped<IKycformRepository, KycformRepository>();
            services.AddScoped<IKycPaymentApprovalRepository, KycPaymentApprovalRepository>();


            services.AddScoped<IDemandDetailsRepository, DemandDetailsRepository>();
            services.AddScoped<IKycdemandpaymentdetailstableaRespository, KycdemandpaymentdetailstableaRepository>();
            services.AddScoped<IKycdemandpaymentdetailstablecRepository, KycdemandpaymentdetailstablecRepository>();

            /* Application Services */


            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
           
           
            
           
            
            services.AddScoped<ILeaseApplicationFormService, LeaseApplicationFormService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            
            services.AddScoped<ILeaseApplicationFormApprovalService, LeaseApplicationFormApprovalService>();
            services.AddScoped<IAllotmentEntryService, AllotmentEntryService>();
            services.AddScoped<ICalculationSheetService, CalculationSheetService>();
            services.AddScoped<IPossesionplanService, PossesionplanService>();
            services.AddScoped<IProceedingEvictionLetterService, ProceedingEvictionLetterService>();
            services.AddScoped<IOldAllotmentEntryService, OldAllotmentEntryService>();
            services.AddScoped<IRequestforproceedingService, RequestforproceedingService>();
            services.AddScoped<ILeasepaymentdetailsService, LeasepaymentdetailsService>();
            services.AddScoped<ILeaseHearingDetailsService, LeaseHearingDetailsService>();
            services.AddScoped<INoticeGenerationService, NoticeGenerationService>();
            services.AddScoped<IJudgementService, JudgementService>();
            services.AddScoped<IAllotteeEvidenceUploadService, AllotteeEvidenceUploadService>();
           
            services.AddScoped<IApplyForServicesService, ApplyForServicesService>();
            services.AddScoped<IHearingdetailsService, HearingdetailsService>();
            services.AddScoped<ICancellationEntryService, CancellationEntryService>();
            
            services.AddScoped<ILeasedeedService, LeasedeedService>();
            services.AddScoped<IActiontakenbyddaService, ActiontakenbyddaService>();
            
           
            services.AddScoped<ITimeextensionService, TimeextensionService>();
           
            services.AddScoped<IAllotmentLetterService,AllotmentLetterService>();
            services.AddScoped<IExtensionApprovalService, ExtensionApprovalService>();
            services.AddScoped<IExtensionService, ExtensionService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();

            services.AddScoped<IKycformService, KycformService>();
            services.AddScoped<IKycformApprovalService, KycformApprovalService>();
            services.AddScoped<IKycPaymentApprovalService, KycPaymentApprovalService>();
            services.AddScoped<IDemandDetailsService, DemandDetailsService>();
            services.AddScoped<IKycdemandpaymentdetailstableaService, KycdemandpaymentdetailstableaService>();
            services.AddScoped<IKycdemandpaymentdetailstablecService, KycdemandpaymentdetailstablecService>();

        }
    }
}