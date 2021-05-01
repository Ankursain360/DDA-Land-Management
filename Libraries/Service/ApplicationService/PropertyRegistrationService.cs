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

        public async Task<List<Locality>> GetLocalityDropDownList2(int divisionId) //added by ishu
        {
            List<Locality> localityList = await _propertyregistrationRepository.GetLocalityDropDownList2(divisionId);
            return localityList;
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
        public async Task<List<Propertyregistration>> GetAllPropertInventorylist(int UserId)
        {
            return await _propertyregistrationRepository.GetAllPropertInventorylist(UserId);
        }

        public async Task<List<Propertyregistration>> GetUnverifiedList(int UserId)
        {
            return await _propertyregistrationRepository.GetUnverifiedList(UserId);
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
            model.InventoriedInId = propertyregistration.InventoriedInId;
            model.PlannedUnplannedLand = propertyregistration.PlannedUnplannedLand;
            model.ClassificationOfLandId = propertyregistration.ClassificationOfLandId;
            model.DepartmentId = propertyregistration.DepartmentId;
            model.PrimaryListNo = propertyregistration.PrimaryListNo;
            model.ZoneId = propertyregistration.ZoneId;
            model.DivisionId = propertyregistration.DivisionId;
            model.LocalityId = propertyregistration.LocalityId;
            model.Colony = propertyregistration.Colony;
            model.Sector = propertyregistration.Sector;
            model.Block = propertyregistration.Block;
            model.Pocket = propertyregistration.Pocket;
            model.PlotNo = propertyregistration.PlotNo;
            model.KhasraNo = propertyregistration.KhasraNo;
            model.Palandmark = propertyregistration.Palandmark;
            model.AreaUnit = propertyregistration.AreaUnit;
            model.TotalAreaInBigha = propertyregistration.TotalAreaInBigha;
            model.TotalAreaInBiswa = propertyregistration.TotalAreaInBiswa;
            model.TotalAreaInBiswani = propertyregistration.TotalAreaInBiswani;
            model.TotalAreaInSqAcreHt = propertyregistration.TotalAreaInSqAcreHt;
            model.TotalArea = propertyregistration.TotalArea;
            model.EncroachmentStatusId = propertyregistration.EncroachmentStatusId;
            model.EncroachedPartiallyFully = propertyregistration.EncroachedPartiallyFully;
            model.EncrochedArea = propertyregistration.EncrochedArea;
            model.BuiltUpEncraochmentArea = propertyregistration.BuiltUpEncraochmentArea;
            model.Vacant = propertyregistration.Vacant;
            model.ActionOnEncroachment = propertyregistration.ActionOnEncroachment;
            model.EncroachAtrfilepath = propertyregistration.EncroachAtrfilepath;
            model.EncraochmentDetails = propertyregistration.EncraochmentDetails;
            model.Boundary = propertyregistration.Boundary;
            model.BoundaryAreaCovered = propertyregistration.BoundaryAreaCovered;
            model.BoundaryDimension = propertyregistration.BoundaryDimension;
            model.BoundaryRemarks = propertyregistration.BoundaryRemarks;
            model.Encroached = propertyregistration.Encroached;
            model.MainLandUseId = propertyregistration.MainLandUseId;
            model.SubUse = propertyregistration.SubUse;
            model.BuiltUp = propertyregistration.BuiltUp;
            model.BuiltUpRemarks = propertyregistration.BuiltUpRemarks;
            model.LayoutFilePath = propertyregistration.LayoutFilePath;
            model.LitigationStatus = propertyregistration.LitigationStatus;
            model.CourtName = propertyregistration.CourtName;
            model.CaseNo = propertyregistration.CaseNo;
            model.OppositeParty = propertyregistration.OppositeParty;
            model.LitigationStatusRemarks = propertyregistration.LitigationStatusRemarks;
            model.GeoReferencing = propertyregistration.GeoReferencing;
            model.GeoFilePath = propertyregistration.GeoFilePath;
            model.GeoLattitude = propertyregistration.GeoLattitude;
            model.GeoLongitude = propertyregistration.GeoLongitude;

            model.TakenOverDepartmentId = propertyregistration.TakenOverDepartmentId;
            model.TakenOverZoneId = propertyregistration.TakenOverZoneId;
            model.TakenOverZoneId = propertyregistration.TakenOverZoneId;
            model.TakenOverName = propertyregistration.TakenOverName;
            model.TakenOverDate = propertyregistration.TakenOverDate;
            model.TakenOverEmailId = propertyregistration.TakenOverEmailId;
            model.TakenOverMobileNo = propertyregistration.TakenOverMobileNo;
            model.TakenOverLandlineNo = propertyregistration.TakenOverLandlineNo;
            model.TakenOverFilePath = propertyregistration.TakenOverFilePath;
            model.TakenOverComments = propertyregistration.TakenOverComments;
            model.HandedOverDepartmentId = propertyregistration.HandedOverDepartmentId;
            model.HandedOverZoneId = propertyregistration.HandedOverZoneId;
            model.HandedOverDivisionId = propertyregistration.HandedOverDivisionId;
            model.HandedOverName = propertyregistration.HandedOverName;
            model.HandedOverDate = propertyregistration.HandedOverDate;
            model.HandedOverEmailId = propertyregistration.HandedOverEmailId;
            model.HandedOverMobileNo = propertyregistration.HandedOverMobileNo;
            model.HandedOverLandlineNo = propertyregistration.HandedOverLandlineNo;
            model.HandedOverFilePath = propertyregistration.HandedOverFilePath;
            model.HandedOverOrderNo = propertyregistration.HandedOverOrderNo;
            model.HandedOverCopyofOrderFilepath = propertyregistration.HandedOverCopyofOrderFilepath;
            model.HandedTransferOrder = propertyregistration.HandedTransferOrder;
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
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Propertyregistration propertyregistration)
        {

            propertyregistration.CreatedBy = 13;
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
        public async Task<List<Department>> GetDepartmentDropDownList()
        {
            List<Department> DepartmentList = await _propertyregistrationRepository.GetDepartmentDropDownList();
            return DepartmentList;
        }

        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model)
        {
            return await _propertyregistrationRepository.GetPropertyRegisterationReportData(model);
        }

        public async Task<PagedResult<Propertyregistration>> GetRestoreLandReportData(PropertyRegisterationSearchDto model)
        {
            return await _propertyregistrationRepository.GetRestoreLandReportData(model);
        }
        public async Task<PagedResult<Propertyregistration>> GetRestorePropertyReportData(PropertyRegisterationSearchDto model)
        {
            return await _propertyregistrationRepository.GetRestorePropertyReportData(model);
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
            return await _propertyregistrationRepository.GetPagedPropertyRegisteration(model, UserId);
        }

        public async Task<List<Department>> GetTakenDepartmentDropDownList()
        {
            return await _propertyregistrationRepository.GetTakenDepartmentDropDownList();
        }

        public async Task<List<Department>> GetHandedDepartmentDropDownList()
        {
            return await _propertyregistrationRepository.GetHandedDepartmentDropDownList();
        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownListMOR()
        {
            return await _propertyregistrationRepository.GetClassificationOfLandDropDownListMOR();
        }

        public async Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisterationMOR(PropertyRegisterationSearchDto model, int userId)
        {
            return await _propertyregistrationRepository.GetPagedPropertyRegisterationMOR(model, userId);
        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownListReport()
        {
            return await _propertyregistrationRepository.GetClassificationOfLandDropDownListReport();
        }

        public string GetEncroachAtr(int id)
        {
            return _propertyregistrationRepository.GetEncroachAtr(id);
        }

        public string GetHandedOverCopyofOrderFile(int id)
        {
            return _propertyregistrationRepository.GetHandedOverCopyofOrderFile(id);
        }

        public async Task<bool> DisposeDetails(int id, Disposedproperty disposedproperty)
        {
            var form = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = form.FirstOrDefault();
            model.DisposalTypeId = disposedproperty.DisposalTypeId;
            model.DisposalDate = disposedproperty.DisposalDate;
            model.DisposalTypeFilePath = disposedproperty.DisposalTypeFilePath;
            model.DisposalComments = disposedproperty.DisposalComments;
            model.IsDisposed = 0;
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> InsertInDisposedProperty(int id, Disposedproperty model)
        {
            model.PropertyRegistrationId = id;
            model.IsDisposed = 0;
            model.DisposedDate = DateTime.Now;
            return await _propertyregistrationRepository.InsertInDisposedProperty(model);
        }

        public async Task<List<Propertyregistration>> GetKhasraReportList()
        {
            return await _propertyregistrationRepository.GetKhasraReportList();
        }

        public async Task<List<Propertyregistration>> GetAllDeletedPropertyList()
        {
            return await _propertyregistrationRepository.GetAllDeletedPropertyList();
        }

        public async Task<PagedResult<Propertyregistration>> GetInventoryUnverifiedVerified(InvnentoryUnverifiedVerifiedSearchDto model, int userId)
        {
            return await _propertyregistrationRepository.GetInventoryUnverifiedVerified(model, userId);
        }

        public async Task<bool> UpdatePropertyRegistrationForLandTransfer(int id, Propertyregistration propertyregistration)
        {
            var result = await _propertyregistrationRepository.FindBy(a => a.Id == id);
            Propertyregistration model = result.FirstOrDefault();
            //model.TakenOverDepartmentId = propertyregistration.TakenOverDepartmentId;
            //model.TakenOverZoneId = propertyregistration.TakenOverZoneId;
            //model.TakenOverDivisionId = propertyregistration.TakenOverDivisionId;
            //model.TakenOverName = propertyregistration.TakenOverName;
            //model.TakenOverDate = propertyregistration.TakenOverDate;
            //model.TakenOverEmailId = propertyregistration.TakenOverEmailId;
            //model.TakenOverMobileNo = propertyregistration.TakenOverMobileNo;
            //model.TakenOverLandlineNo = propertyregistration.TakenOverLandlineNo;
            //model.TakenOverComments = propertyregistration.TakenOverComments;

            //model.HandedOverDepartmentId = propertyregistration.HandedOverDepartmentId;
            //model.HandedOverZoneId = propertyregistration.HandedOverZoneId;
            //model.HandedOverDivisionId = propertyregistration.HandedOverDivisionId;
            //model.HandedOverName = propertyregistration.HandedOverName;
            //model.HandedOverDate = propertyregistration.HandedOverDate;
            //model.HandedOverEmailId = propertyregistration.HandedOverEmailId;
            //model.HandedOverMobileNo = propertyregistration.HandedOverMobileNo;
            //model.HandedOverLandlineNo = propertyregistration.HandedOverLandlineNo;
            //model.HandedOverComments = propertyregistration.HandedOverComments;


            model.DepartmentId = propertyregistration.DepartmentId;
            model.ZoneId = propertyregistration.ZoneId;
            model.DivisionId = propertyregistration.DivisionId;
            model.ModifiedDate = DateTime.Now;
            _propertyregistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Propertyregistration>> GetDeletedLandReportData(PropertyRegisterationSearchDto model)
        {
            return await _propertyregistrationRepository.GetDeletedLandReportData( model);
        }
    }
}
