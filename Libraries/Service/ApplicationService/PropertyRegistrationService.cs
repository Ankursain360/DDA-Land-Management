using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{

    public class PropertyRegistrationService : EntityService<Propertyregistration>, IPropertyRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyRegistrationRepository _propertyregistrationRepository;

        public PropertyRegistrationService(IUnitOfWork unitOfWork, IPropertyRegistrationRepository propertyregistrationRepository)
        : base(unitOfWork, propertyregistrationRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyregistrationRepository = propertyregistrationRepository;

        }
        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            List<Classificationofland> ClassificationoflandList = await _propertyregistrationRepository.GetClassificationOfLandDropDownList();
            return ClassificationoflandList;
        }
        public async Task<List<Zone>> GetZoneDropDownList(int DepartmentId)
        {
            List<Zone> zoneList = await _propertyregistrationRepository.GetZoneDropDownList(DepartmentId);
            return zoneList;
        }

        public async Task<List<Locality>> GetLocalityDropDownList(int zoneId)
        {
            List<Locality> LocalityList = await _propertyregistrationRepository.GetLocalityDropDownList(zoneId);
            return LocalityList;
        }
     
        public async Task<List<Propertyregistration>> GetPrimaryListNoList(int DivisionId)
        {
            List<Propertyregistration> PrimaryListNoList = await _propertyregistrationRepository.GetPrimaryListNoList(DivisionId);
            return PrimaryListNoList;
        }
        public async Task<List<Landuse>> GetLandUseDropDownList()
        {
            List<Landuse> LanduseList = await _propertyregistrationRepository.GetLandUseDropDownList();
            return LanduseList;
        }
        public async Task<List<Disposaltype>> GetDisposalTypeDropDownList()
        {
            List<Disposaltype> DisposaltypeList = await _propertyregistrationRepository.GetDisposalTypeDropDownList();
            return DisposaltypeList;
        }

        public async Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId)
        {
            return await _propertyregistrationRepository.GetAllPropertyregistration(UserId);
        }

        public async Task<Propertyregistration> FetchSingleResult(int id)
        {
            var result = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Propertyregistration propertyregistration)
        {
            var result = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = result.FirstOrDefault();
            model.ClassificationOfLandId = propertyregistration.ClassificationOfLandId;
            model.DepartmentId = propertyregistration.DepartmentId;
            model.PrimaryListNo = propertyregistration.PrimaryListNo;
            model.ZoneId = propertyregistration.ZoneId;
            model.DivisionId = propertyregistration.DivisionId;
            model.LocalityId = propertyregistration.LocalityId;
            model.KhasraNo = propertyregistration.KhasraNo;
            model.Palandmark = propertyregistration.Palandmark;
            model.EncroachmentStatusId = propertyregistration.EncroachmentStatusId;
            model.EncraochmentDetails = propertyregistration.EncraochmentDetails;
            model.Boundary = propertyregistration.Boundary;
            model.BoundaryRemarks = propertyregistration.BoundaryRemarks;
            model.TotalAreaInBigha = propertyregistration.TotalAreaInBigha;
            model.TotalArea = propertyregistration.TotalArea;
            model.Encroached = propertyregistration.Encroached;
            model.BuiltUpEncraochmentArea = propertyregistration.BuiltUpEncraochmentArea;
            model.Vacant = propertyregistration.Vacant;
            model.PlannedUnplannedLand = propertyregistration.PlannedUnplannedLand;
            model.MainLandUseId = propertyregistration.MainLandUseId;
            model.SubUse = propertyregistration.SubUse;
            model.BuiltUp = propertyregistration.BuiltUp;
            model.BuiltUpRemarks = propertyregistration.BuiltUpRemarks;
            model.LayoutPlan = propertyregistration.LayoutPlan;
            model.LayoutFilePath = propertyregistration.LayoutFilePath;
            model.LitigationStatus = propertyregistration.LitigationStatus;
            model.LitigationStatusRemarks = propertyregistration.LitigationStatusRemarks;
            model.GeoReferencing = propertyregistration.GeoReferencing;
            model.GeoFilePath = propertyregistration.GeoFilePath;
            model.TakenOverName = propertyregistration.TakenOverName;
            model.TakenOverDate = propertyregistration.TakenOverDate;
            model.TakenOverEmailId = propertyregistration.TakenOverEmailId;
            model.TakenOverMobileNo = propertyregistration.TakenOverMobileNo;
            model.TakenOverLandlineNo = propertyregistration.TakenOverLandlineNo;
            model.TakenOverFilePath = propertyregistration.TakenOverFilePath;
            model.TakenOverComments = propertyregistration.TakenOverComments;
            model.HandedOverName = propertyregistration.HandedOverName;
            model.HandedOverDate = propertyregistration.HandedOverDate;
            model.HandedOverEmailId = propertyregistration.HandedOverEmailId;
            model.HandedOverMobileNo = propertyregistration.HandedOverMobileNo;
            model.HandedOverLandlineNo = propertyregistration.HandedOverLandlineNo;
            model.HandedOverFilePath = propertyregistration.HandedOverFilePath;
            model.HandedOverComments = propertyregistration.HandedOverComments;
            model.DisposalTypeId = propertyregistration.DisposalTypeId;
            model.DisposalDate = propertyregistration.DisposalDate;
            model.DisposalTypeFilePath = propertyregistration.DisposalTypeFilePath;
            model.DisposalComments = propertyregistration.DisposalComments;
            model.Remarks = propertyregistration.Remarks;
            model.IsValidate = propertyregistration.IsValidate;
            model.IsActive = propertyregistration.IsDeleted;
            model.IsActive = propertyregistration.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Propertyregistration propertyregistration)
        {

            propertyregistration.CreatedBy = 1;
            propertyregistration.CreatedDate = DateTime.Now;
            _propertyregistrationRepository.Add(propertyregistration);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = form.FirstOrDefault();
            model.IsDeleted = 0;
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Restore(int id)  // added by ishu
        {
            var form = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = form.FirstOrDefault();
            model.ModifiedBy = 1;
            
            model.IsDeleted = 1;
            model.ModifiedDate = DateTime.Now;
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public string GetFile(int id)
        {
            return _propertyregistrationRepository.GetFile(id);
        }
        public string GetGeoFile(int id)
        {
            return _propertyregistrationRepository.GetGeoFile(id);
        }

        public Task<bool> CheckDeleteAuthority(int id)
        {
            return _propertyregistrationRepository.CheckDeleteAuthority(id);
        }

        public async Task<List<Department>> GetDepartmentDropDownList()
        {
            List<Department> DepartmentList = await _propertyregistrationRepository.GetDepartmentDropDownList();
            return DepartmentList;
        }

        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationSearchDto model, int classificationofland, int department, int zone, int division, int locality, string plannedUnplannedLand, int mainLandUse, int litigation, int encroached)
        {
            return await _propertyregistrationRepository.GetPropertyRegisterationReportData( model, classificationofland,  department,  zone,  division,  locality,  plannedUnplannedLand,  mainLandUse,  litigation,  encroached);
        }

        public async Task<List<Propertyregistration>> GetRestoreLandReportData(int department, int zone, int division,int primaryListNo)
        {
            return await _propertyregistrationRepository.GetRestoreLandReportData(department, zone, division,primaryListNo);
        }


        public async Task<List<Division>> GetDivisionDropDownList(int zoneId)
        {
            List<Division> DivisionList = await _propertyregistrationRepository.GetDivisionDropDownList(zoneId);
            return DivisionList;
        }

        public string GetDisposalFile(int id)
        {
            return _propertyregistrationRepository.GetDisposalFile(id);
        }

        public string GetHandedOverFile(int id)
        {
            return _propertyregistrationRepository.GetHandedOverFile(id);
        }

        public string GetTakenOverFile(int id)
        {
            return _propertyregistrationRepository.GetTakenOverFile(id);
        }

        public async Task<bool> InsertInDeletedProperty(int id, Deletedproperty model)
        {
            model.PropertyRegistrationId = id;
            model.IsDeleted = 0;
            model.DeletedBy = 1;
            model.DeletedDate = DateTime.Now;
            return await _propertyregistrationRepository.InsertInDeletedProperty(model);
        }
        public async Task<bool> InsertInRestoreProperty(int id, Restoreproperty model)
        {
            model.PropertyRegistrationId = id;
            //model.IsDeleted = 0;
            model.RestoreBy = 1;
            model.RestoreDate = DateTime.Now;
            return await _propertyregistrationRepository.InsertInRestoreProperty(model);
        }

        public async Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId)
        {
            return await _propertyregistrationRepository.GetPagedPropertyRegisteration(model,  UserId);
        }
    }
}
