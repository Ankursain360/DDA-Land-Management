﻿using GIS.Helper;
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

namespace GIS.Infrastructure.Extensions
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
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<ILocalityRepository, LocalityRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRebateRepository, RebateRepository>();
            services.AddScoped<ILandUseRepository, LandUseRepository>();
            services.AddScoped<IClassificationOfLandRepository, ClassificationOfLandRepository>();
            services.AddScoped<IWorkflowTemplateRepository, WorkflowTemplateRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IDemolitionchecklistRepository, DemolitionchecklistRepository>();
            services.AddScoped<IDemolitiondocumentRepository, DemolitiondocumentRepository>();
            services.AddScoped<IDemolitionprogrammasterRepository, DemolitionprogrammasterRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IStructureRepository, StructureRepository>();
            services.AddScoped<ICaseyearRepository, CaseyearRepository>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IApprovalstatusRepository, ApprovalstatusRepository>();
            services.AddScoped<IGISSRepository, GISRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAcquiredlandvillageRepository,  AcquiredlandvillageRepository>();
            services.AddScoped<IKhasraRepository , KhasraRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IApplicationModificationDetailsRepository, ApplicationModificationDetailsRepository>();
            /* Application Services */

            services.AddScoped<IApplicationModificationDetailsService, ApplicationModificationDetailsService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IKhasraService,  KhasraService>();
            services.AddScoped<IAcquiredlandvillageService, AcquiredlandvillageService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<ILocalityService, LocalityService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IRebateService, RebateService>();
            services.AddScoped<ILandUseService, LandUseService>();
            services.AddScoped<IClassificationOfLandService, ClassificationOfLandService>();
            services.AddScoped<IWorkflowTemplateService, WorkflowTemplateService>();
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IDemolitionchecklistService, DemolitionchecklistService>();
            services.AddScoped<IDemolitiondocumentService, DemolitiondocumentService>();
            services.AddScoped<IDemolitionprogrammasterService, DemolitionprogrammasterService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IStructureService, StructureService>();
            services.AddScoped<ICaseyearService, CaseyearService>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IGISService, GISService>();
            
        }
    }
}
