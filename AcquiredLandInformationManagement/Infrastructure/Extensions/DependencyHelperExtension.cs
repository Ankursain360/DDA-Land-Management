using Microsoft.Extensions.DependencyInjection;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Libraries.Service.IApplicationService;
using Libraries.Service.ApplicationService;
using Libraries.Repository.Common;
using Service.ApplicationService;

namespace AcquiredLandInformationManagement.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
		{
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IJointsurveyRepository, JointsurveyRepository>();


            services.AddScoped<IAcquiredlandvillageRepository, AcquiredlandvillageRepository>();
            services.AddScoped<ISchemeRepository, SchemeRepository>();

            services.AddScoped<IUndersection4Repository, Undersection4Repository>();

            services.AddScoped<IProposaldetailsRepository, ProposaldetailsRepository>();
            services.AddScoped<IProposalplotdetailsRepository, ProposalplotdetailsRepository>();
            services.AddScoped<IKhasraRepository, KhasraRepository>();
            services.AddScoped<IUnderSection4PlotRepository, Undersection4plotRepository>();

            services.AddScoped<IUndersection22Repository, Undersection22Repository>();
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


            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IJointsurveyService, JointsurveyService>();
            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<IUndersection4service, Undersection4Service>();
            services.AddScoped<IProposaldetailsService, ProposaldetailsService>(); 
            services.AddScoped<IProposalplotdetailsService, ProposalplotdetailsService>();
            services.AddScoped<IKhasraService, KhasraService>();
            services.AddScoped<IUndersection4PlotService, Undersection4PlotService>();
            services.AddScoped<IUndersection22Service, Undersection22Service>();
            services.AddScoped<IAwardplotDetailService, AwardplotDetailsService>();
            services.AddScoped<IDisposallandtypeService, DisposallandtypeService>();
            services.AddScoped<INazulService, NazulService>();
            services.AddScoped<IDisposallandService, DisposallandService>();
            services.AddScoped<IMorlandService, MorlandService>();

            services.AddScoped<IEnhancecompensationService, EnhancecompensationService>(); //added by Nikita
            services.AddScoped<IEnchroachmentService, EnchroachmentService>(); //added by Nikita


            services.AddScoped<ILdolandService, LdolandService>();

            services.AddScoped<IBooktransferlandService, BooktransferlandService>();
            services.AddScoped<ISakanidetailService, SakanidetailService>(); //added by Nikita



        }
    }
}