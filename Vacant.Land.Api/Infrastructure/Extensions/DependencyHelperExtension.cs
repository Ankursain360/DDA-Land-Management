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
using Vacant.Land.Api.Helper;

namespace Vacant.Land.Api.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          //  services.AddScoped<ISiteContext, SiteContext>();

            /* Respository */
            services.AddScoped<INazullandRepository, NazullandRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            /*Land transfer */
            services.AddScoped<INazullandRepository, NazullandRepository>();
            services.AddScoped<ILandTransferRepository, LandtransferRepository>();
            services.AddScoped<ICurrentstatusoflandhistoryRepository, CurrentstatusoflandhistoryRepository>();
            services.AddScoped<IPropertyRegistrationRepository, PropertyRegistrationRepository>();
            services.AddScoped<IPlanningRepositry, PlanningRepositry>();
            services.AddScoped<IInsertVacantLandImagesRepository, InsertVacantLandImagesRepository>();
            services.AddScoped<IWatchWardAPIRepository, WatchWardAPIRepository>();
            services.AddScoped<IWatchandwardRepository, WatchandwardRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IEncroachmentRegisterAPIRepository, EncroachmentRegisterAPIRepository>();
            services.AddScoped<IEncroachmentRegisterationRepository, EncroachmentRegisterationRepository>();
            services.AddScoped<IVlmsmobileappaccesslogRepository, VlmsmobileappaccesslogRepository>();
            services.AddScoped<IPossessiondetailsRepository, PossessiondetailsRepository>();
            /* Application Services */
            services.AddScoped<IPossessiondetailsService, PossessiondetailsService>();
            services.AddScoped<IVlmsmobileappaccesslogService, VlmsmobileappaccesslogService>();
            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IWatchWardAPIService, WatchWardAPIService>();
            services.AddScoped<IWatchandwardService, WatchandwardService>();
            services.AddScoped<IEncroachmentRegisterAPIService, EncroachmentRegisterAPIService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IEncroachmentRegisterationService, EncroachmentRegisterationService>();

            /* Land transfer Services */

            services.AddScoped<INazullandService, NazullandService>();
            services.AddScoped<ILandTransferService, LandTransferService>();
            services.AddScoped<ICurrentstatusoflandhistoryService, CurrentstatusoflandhistoryService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IPlanningService, PlanningService>();
            services.AddScoped<IInsertVacantLandImagesService, InsertVacantLandImagesService>();

        }
    }
}