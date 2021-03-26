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
            services.AddScoped<IPremiumrateRepository, PremiumrateRepository>();
            services.AddScoped<IDocumentchargesRepository, DocumentchargesRepository>();
            services.AddScoped<IGroundRentRepository, GroundRentRepository>();
            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<ILicenceFeesRepository, LicenceFeesRepository>();
            services.AddScoped<IInterestrateRepository, InterestrateRepository>();
            services.AddScoped<IDocumentCheckListRepository, DocumentCheckListRepository>();
            services.AddScoped<ILeaseApplicationFormRepository, LeaseApplicationFormRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
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
            services.AddScoped<IJudgementRepository, JudgementRepository>();
            services.AddScoped<IAllotteeEvidenceUploadRepository, AllotteeEvidenceUploadRepository>();
            services.AddScoped<ILeasepurposeRepository, LeasepurposeRepository>();
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
            services.AddScoped<IPremiumrateService, PremiumrateService>();
            services.AddScoped<IDocumentchargesServices, DocumentchargesServices>();
            services.AddScoped<IGroundRentService, GroundRentService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<ILicenceFeesService, LicenceFeesService>();
            services.AddScoped<IInterestrateService, InterestrateService>();
            services.AddScoped<IDocumentCheckListService, DocumentCheckListService>();
            services.AddScoped<ILeaseApplicationFormService, LeaseApplicationFormService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
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
            services.AddScoped<ILeasepurposeService, LeasepurposeService>();

        }
    }
}