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
using NewLandAcquisition.Helper;
namespace NewLandAcquisition.Infrastructure.Extensions
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
          
            services.AddScoped<INewlandnotificationRepository, NewlandnotificationRepository>();

            services.AddScoped<INewLandProposalPlotDetailsRepository, NewLandProposalPlotDetailsRepository>();
            services.AddScoped<INewLandPaymentDetailRepository, NewLandPaymentDetailRepository>();

            services.AddScoped<INewlandus4plotRepository, Newlandus4plotRepository>();
            services.AddScoped<INewLandEnhanceCompensationRepository, NewLandEnhanceCompensationRepository>();

            services.AddScoped<INewlandus6plotRepository, Newlandus6plotRepository>();
            services.AddScoped<INewlandus17plotRepository, Newlandus17plotRepository>();





            services.AddScoped<INewlandus22plotRepository, Newlandus22plotRepository>();
           
            
            services.AddScoped<InewlandawardplotdetailsRepository, NewlandawardplotdetailsRepository>();
            services.AddScoped<INewlandAppealdetailRepository, NewlandAppealdetailRepository>();

            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();

            services.AddScoped<INewLandJointSurveyRepository, NewLandJointSurveyRepository>();

            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestApprovalProcessRepository, RequestApprovalProcessRepository>();
            services.AddScoped<INewlandpossesiondetailsRepository, NewlandpossesiondetailsRepository>();
            services.AddScoped<INewlandannexure1Repository, Newlandannexure1Repository>();
            services.AddScoped<INewlandannexure2Repository, Newlandannexure2Repository>();
            services.AddScoped<INewlandnotificationdetailsRepository, NewlandnotificationdetailsRepository>();
            services.AddScoped<INewlandvillageRepository, NewlandVillageRepository>();
            services.AddScoped<INewlandkhasraRepository,NewlandkhasraRepository>();
            services.AddScoped<INewlandawardmasterdetailRepository, NewlandawardmasterdetailRepository>();


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
           
            services.AddScoped<INewLandProposalPlotDetailsService, NewLandProposalPlotDetailsService>();
            services.AddScoped<INewLandPaymentdetailService, NewLandPaymentdetailService>();

            services.AddScoped<INewlandus4plotService, Newlandus4plotService>();
            services.AddScoped<INewLandEnhanceCompensationService, NewLandEnhanceCompensationService>();
            services.AddScoped<INewlandawardplotdetailsService, NewlandawardplotdetailsService>();


            services.AddScoped<INewlandus6plotService, Newlandus6plotService>();
            services.AddScoped<INewlandus17plotService, Newlandus17plotService>();


            services.AddScoped<INewlandus22plotService, Newlandus22plotService>();
            services.AddScoped<INewlandAppealdetailservice, NewlandAppealdetailService>();
            services.AddScoped<INewlandnotificationService, NewlandnotificationService>();


            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();

            services.AddScoped<INewLandJointSurveyService, NewLandJointSurveyService>();

            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestApprovalProcessService, RequestApprovalProcessService>();
            services.AddScoped<INewlandpossessiondetailsService, NewlandpossesiondetailsService>();
            services.AddScoped<INewlandannexure1Service, Newlandannexure1Service>();
            services.AddScoped<INewlandannexure2Service, Newlandannexure2Service>();
            services.AddScoped<INewlandnotificationdetailsService, NewlandnotificationdetailsService>();
            services.AddScoped<INewlandvillageService, NewlandvillageService>();
            services.AddScoped<INewlandkhasraService, NewlandkhasraService>();
            services.AddScoped<INewlandawardmasterdetailService, NewlandawardmasterdetailsService>();

        }
    }
}