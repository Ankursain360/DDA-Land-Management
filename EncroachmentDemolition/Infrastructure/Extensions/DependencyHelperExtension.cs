using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.IApplicationService;
using Microsoft.Extensions.DependencyInjection;
using Libraries.Service.ApplicationService;

namespace EncroachmentDemolition.Infrastructure.Extensions
{
    public static class DependencyHelperExtension
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            /* Common Dependencies */
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            /* Respository */
            services.AddScoped<IEncroachmentRegisterationRepository, EncroachmentRegisterationRepository>();

            /* Application Services */
            services.AddScoped<IEncroachmentRegisterationService, EncroachmentRegisterationService>();
        }
    }
}