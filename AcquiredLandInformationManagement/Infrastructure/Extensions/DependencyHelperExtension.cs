using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;
using AcquiredLandInformationManagement.Helper;
using Service.IApplicationService;
using Repository.IEntityRepository;
using Repository.EntityRepository;

namespace AcquiredLandInformationManagement.Infrastructure.Extensions
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
            services.AddScoped<IJointsurveyRepository, JointsurveyRepository>();
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();
            services.AddScoped<IGramsabhalandRepository, GramsabhalandRepository>();
          
            services.AddScoped<IUndersection6plotRepository, Undersection6plotRepository>();


            services.AddScoped<IProposaldetailsRepository, ProposaldetailsRepository>();
           
            services.AddScoped<IProposalplotdetailsRepository, ProposalplotdetailsRepository>();
           
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();

           
            services.AddScoped<IAwardplotDetailsRepository, AwardplotDetailsRepository>();
           
            services.AddScoped<IDisposallandtypeRepository, DisposallandtypeRepository>();
            services.AddScoped<INazulRepository, NazulRepository>();
            services.AddScoped<IDisposallandRepository, DisposallandRepository>();
            services.AddScoped<IMorlandRepository, MorlandRepository>();
            services.AddScoped<IEnhancecompensationRepository, EnhancecompensationRepository>(); //added by Nikita
            services.AddScoped<IEnchroachmentRepository, EnchroachmentRepository>(); //added by Nikita

            services.AddScoped<ILdolandRepository, LdolandRepository>();
            services.AddScoped<IBooktransferlandRepository, BooktransferlandRepository>();
            services.AddScoped<ISakanidetailRepository, SakanidetailRepository>(); //added by Nikita
            services.AddScoped<IJaraidetailRepository, JaraidetailRepository>(); //added by Nikita
           
            services.AddScoped<IModuleRepository, ModuleRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
           
            services.AddScoped<IDisposallandRepository, DisposallandRepository>();//added by anuj 10-feb-21
            services.AddScoped<IUndersection22plotdetailsRepository, Undersection22plotdetailsRepository>();
            services.AddScoped<IUndersection17plotdetailRepository, Undersection17plotdetailRepository>();

            services.AddScoped<IAppealdetailRepository, AppealdetailRepository>();
            services.AddScoped<IPaymentdetailRepository, PaymentdetailRepository>();


            services.AddScoped<IPossessiondetailsRepository, PossessiondetailsRepository>();
            services.AddScoped<IDemandListDetailsRepository, DemandListDetailsRepository>();
            services.AddScoped<IMutationRepository, MutationRepository>();
            services.AddScoped<INewlandkhasraRepository, NewlandkhasraRepository>();
            services.AddScoped<INewlandnotificationRepository, NewlandnotificationRepository>();
            services.AddScoped<IKhasraRepository, KhasraRepository>();
            services.AddScoped<IAwardmasterdetailsRepository, AwardmasterdetailsRepository>();

            services.AddScoped<IUndersection4Repository, Undersection4Repository>();
            services.AddScoped<IUnderSection6Repository, UnderSection6Repository>();
            services.AddScoped<IUndersection17Repository, Undersection17Repository>();
            services.AddScoped<IUndersection22Repository, Undersection22Repository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            /* Application Services */

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IJointsurveyService, JointsurveyService>();
            services.AddScoped<IGramsabhalandService, GramsabhalandService>();


            services.AddScoped<IProposaldetailsService, ProposaldetailsService>();

            services.AddScoped<IProposalplotdetailsService, ProposalplotdetailsService>();
          
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();
           
            services.AddScoped<IAwardplotDetailService, AwardplotDetailsService>();
            services.AddScoped<IDisposallandtypeService, DisposallandtypeService>();
            services.AddScoped<INazulService, NazulService>();
            services.AddScoped<IDisposallandService, DisposallandService>();
            services.AddScoped<IMorlandService, MorlandService>();
            services.AddScoped<IUndersection6plotService, Undersection6plotService>();
            services.AddScoped<IEnhancecompensationService, EnhancecompensationService>(); //added by Nikita
            services.AddScoped<IEnchroachmentService, EnchroachmentService>(); //added by Nikita


            services.AddScoped<ILdolandService, LdolandService>();

            services.AddScoped<IBooktransferlandService, BooktransferlandService>();
            services.AddScoped<ISakanidetailService, SakanidetailService>(); //added by Nikita
            services.AddScoped<IJaraidetailService, JaraidetailService>();
           
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();

            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
          
            services.AddScoped<IDisposallandService, DisposallandService>();//added by anuj 10-feb-21
            services.AddScoped<IUndersection22plotdetailsService, Undersection22plotdetailsService>();
            services.AddScoped<IUndersection17plotdetailService, Undersection17plotdetailService>();

            services.AddScoped<IAppealdetailService, AppealdetailService>();
            services.AddScoped<IPaymentdetailService, PaymentdetailService>();
            services.AddScoped<IPossessiondetailsService, PossessiondetailsService>();
          
           
            services.AddScoped<IDemandListDetailsService, DemandListDetailsService>();
            services.AddScoped<IMutationService, MutationService>();
           
            services.AddScoped<INewlandkhasraService, NewlandkhasraService>();
            services.AddScoped<INewlandnotificationService, NewlandnotificationService>();
            services.AddScoped<IKhasraService, KhasraService>();
            services.AddScoped<IAwardmasterdetailsService, AwardmasterdetailsService>();

            services.AddScoped<IUndersection4service, Undersection4Service>();
            services.AddScoped<IUnderSection6Service, UnderSection6Service>();
            services.AddScoped<IUndersection17Service, Undersection17Service>();
            services.AddScoped<IUndersection22Service, Undersection22Service>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
        }
    }
}