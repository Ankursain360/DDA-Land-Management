using CourtCasesManagement.Helper;
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

namespace CourtCasesManagement.Infrastructure.Extensions
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
           
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
           
            services.AddScoped<IApprovalProccessRepository, ApprovalProccessRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<ILawyerRepository,LawyerRespository>();
            services.AddScoped<ILegalmanagementsystemRepository, LegalmanagementsystemRepository>();
            services.AddScoped<ICasenatureRepository, CasenatureRepository>();
            services.AddScoped<ICaseyearRepository, CaseyearRepository>();

            /* Application Services */
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IActionsService, ActionsService>();
         
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IApprovalProccessService, ApprovalProccessService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<ILawyerService, LawyerService>();
            services.AddScoped<ILegalmanagementsystemservice, Legalmanagementsystemservice>();
            services.AddScoped<ICasenatureService, CasenatureService>();
            services.AddScoped<ICaseyearService, CaseyearService>();
        }
    }
}
