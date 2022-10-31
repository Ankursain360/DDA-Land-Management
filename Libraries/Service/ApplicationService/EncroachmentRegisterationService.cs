using Dto.Search;
using Libraries.Model.Common;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class EncroachmentRegisterationService:EntityService<EncroachmentRegisteration>, IEncroachmentRegisterationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncroachmentRegisterationRepository _encroachmentRegisterationRepository;
        public EncroachmentRegisterationService(IUnitOfWork unitOfWork, IEncroachmentRegisterationRepository encroachmentRegisterationRepository)
      : base(unitOfWork, encroachmentRegisterationRepository)
        {
            _unitOfWork = unitOfWork;
            _encroachmentRegisterationRepository = encroachmentRegisterationRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = form.FirstOrDefault();
            model.IsActive = 0;
            _encroachmentRegisterationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<EncroachmentRegisteration> FetchSingleResult(int id)
        {
            return await _encroachmentRegisterationRepository.FetchSingleResult(id);
        }
        //
        //Task<List<EncroachmentRegisteration>> GetAllDownloadEncroachment();
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _encroachmentRegisterationRepository.GetAllDepartment();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllDownloadEncroachment()
        {
            return await _encroachmentRegisterationRepository.GetAllDownloadEncroachment(); 
        }
        public async Task<List<EncroachmentRegisteration>> GetAllDownloadEncroachmentList(DemolitionReportSearchDto model)
        {
            return await _encroachmentRegisterationRepository.GetAllDownloadEncroachmentList(model);
        }
        public async Task<List<Locality>> GetAllLocalityList()//for demolition report -- ishu
        {
            return await _encroachmentRegisterationRepository.GetAllLocalityList();
        }
        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            return await _encroachmentRegisterationRepository.GetAllDivision(zone);
        }
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _encroachmentRegisterationRepository.GetAllEncroachmentRegisteration();
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _encroachmentRegisterationRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _encroachmentRegisterationRepository.GetAllZone(departmentId);
        }
        public async Task<List<Watchandward>> GetAllEncroachmentRegisterlist(int approved)
        {
            return await _encroachmentRegisterationRepository.GetAllEncroachmentRegisterlist(approved);
        }
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisterlistForDownload()
        {
            return await _encroachmentRegisterationRepository.GetAllEncroachmentRegisterlistForDownload(); 
        }
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisterlistForDownload2(InspectionEncroachmentregistrationSearchDto model)
        {
            return await _encroachmentRegisterationRepository.GetAllEncroachmentRegisterlistForDownload2( model);
        }
        public async Task<PagedResult<Watchandward>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model, int approved, int zoneId)
        {
            return await _encroachmentRegisterationRepository.GetPagedEncroachmentRegisteration(model, approved, zoneId);
        }
        public async Task<bool> Update(int id, EncroachmentRegisteration encroachmentRegisteration)
        {
            var result = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();
            model.AreaUnit = encroachmentRegisteration.AreaUnit;
            model.TotalAreaInSqAcreHt = encroachmentRegisteration.TotalAreaInSqAcreHt;
            model.Area = encroachmentRegisteration.Area;
            model.LocationAddressWithLandMark = encroachmentRegisteration.LocationAddressWithLandMark;
            model.DepartmentId = encroachmentRegisteration.DepartmentId;
            model.DivisionId = encroachmentRegisteration.DivisionId;
            model.EncrochmentDate = encroachmentRegisteration.EncrochmentDate;
            model.IsPossession = encroachmentRegisteration.IsPossession;
            model.KhasraNo = encroachmentRegisteration.KhasraNo;
            model.LocalityId = encroachmentRegisteration.LocalityId;
            model.OtherDepartment = encroachmentRegisteration.OtherDepartment;
            model.PoliceStation = encroachmentRegisteration.PoliceStation;
            model.PossessionType = encroachmentRegisteration.PossessionType;
            model.Remarks = encroachmentRegisteration.Remarks;
            model.SecurityGuardOnDuty = encroachmentRegisteration.SecurityGuardOnDuty;
            model.StatusOfLand = encroachmentRegisteration.StatusOfLand;
            model.ZoneId = encroachmentRegisteration.ZoneId;
            model.EncroacherName = encroachmentRegisteration.EncroacherName;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = 1;
            _encroachmentRegisterationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(EncroachmentRegisteration encroachmentRegisteration)
        {
            encroachmentRegisteration.CreatedDate = DateTime.Now;
            _encroachmentRegisterationRepository.Add(encroachmentRegisteration);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Khasra>> GetAllKhasraList(int localityId)
        {
            return await _encroachmentRegisterationRepository.GetAllKhasraList(localityId);
        }


        public async Task<List<Propertyregistration>> GetAllKhasraListFromPropertyInventory(int ZoneId, int DepartmentId)
        {
            return await _encroachmentRegisterationRepository.GetAllKhasraListFromPropertyInventory(ZoneId,DepartmentId);
        }

        public async Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment)
        {
            detailsOfEncroachment.CreatedBy = 1;
            detailsOfEncroachment.CreatedDate = DateTime.Now;
            detailsOfEncroachment.IsActive = 1;
            return await _encroachmentRegisterationRepository.SaveDetailsOfEncroachment(detailsOfEncroachment);
        }
        public async Task<bool> DeleteDetailsOfEncroachment(int Id)
        {
            return await _encroachmentRegisterationRepository.DeleteDetailsOfEncroachment(Id);
        }
        public async Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails)
        {
            encroachmentFirFileDetails.CreatedBy = 1;
            encroachmentFirFileDetails.CreatedDate = DateTime.Now;
            encroachmentFirFileDetails.IsActive = 1;
            return await _encroachmentRegisterationRepository.SaveEncroachmentFirFileDetails(encroachmentFirFileDetails);
        }

        public async Task<bool> DeleteEncroachmentFirFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.DeleteEncroachmentFirFileDetails(Id);
        }

        public async Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails)
        {
            encroachmentPhotoFileDetails.CreatedBy = 1;
            encroachmentPhotoFileDetails.CreatedDate = DateTime.Now;
            encroachmentPhotoFileDetails.IsActive = 1;
            return await _encroachmentRegisterationRepository.SaveEncroachmentPhotoFileDetails(encroachmentPhotoFileDetails);
        }

        public async Task<bool> DeleteEncroachmentPhotoFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.DeleteEncroachmentPhotoFileDetails(Id);
        }

        public async Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails)
        {
            encroachmentLocationMapFileDetails.CreatedBy = 1;
            encroachmentLocationMapFileDetails.CreatedDate = DateTime.Now;
            encroachmentLocationMapFileDetails.IsActive = 1;
            return await _encroachmentRegisterationRepository.SaveEncroachmentLocationMapFileDetails(encroachmentLocationMapFileDetails);
        }

        public async Task<bool> DeleteEncroachmentLocationMapFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.DeleteEncroachmentLocationMapFileDetails(Id);
        }

        public async Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int Id)
        {
            return await _encroachmentRegisterationRepository.GetDetailsOfEncroachment(Id);
        }

        public async Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.GetEncroachmentPhotoFileDetails(Id);
        }

        public async Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.GetEncroachmentLocationMapFileDetails(Id);
        }

        public async Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int Id)
        {
            return await _encroachmentRegisterationRepository.GetEncroachmentFirFileDetails(Id);
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto enchroachmentSearchDto)
        {
            return await _encroachmentRegisterationRepository.GetEncroachmentReportData(enchroachmentSearchDto);
        }
        public async Task<List<EncroachmentRegisteration>> GetAllDownloadEncroachmentList(EnchroachmentSearchDto dto)
        {
            return await _encroachmentRegisterationRepository.GetAllDownloadEncroachmentList(dto);
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(InspectionEncroachmentregistrationSearchDto inspectionEncroachmentregistrationSearchDto)
        {
            return await _encroachmentRegisterationRepository.GetEncroachmentRegisterationReportData(inspectionEncroachmentregistrationSearchDto);
        }

        public async Task<bool> UpdateBeforeApproval(int id, EncroachmentRegisteration encroachmentRegisterations)
        {
            var result = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();

            model.ApprovedStatus = encroachmentRegisterations.ApprovedStatus;
            model.PendingAt = encroachmentRegisterations.PendingAt;
            _encroachmentRegisterationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedDemolitionReport(DemolitionReportSearchDto model)
        {
            return await _encroachmentRegisterationRepository.GetPagedDemolitionReport(model);  //demolition report ---ishu
        }

        public async Task<bool> RollBackEntry(int id)
        {
            var result = await _encroachmentRegisterationRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();
            _encroachmentRegisterationRepository.Delete(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> RollBackEntryEncroachmentLocationMapFileDetails(int id)
        {
            return await _encroachmentRegisterationRepository.RollBackEntryEncroachmentLocationMapFileDetails(id);
        }

        public async Task<bool> RollBackEntryEncroachmentFirFileDetails(int id)
        {
            return await _encroachmentRegisterationRepository.RollBackEntryEncroachmentFirFileDetails(id);
        }

        public async Task<bool> RollBackEntryEncroachmentPhotoFileDetails(int id)
        {
            return await _encroachmentRegisterationRepository.RollBackEntryEncroachmentPhotoFileDetails(id);
        }

        public async Task<bool> RollBackEntryDetailsofEncroachmentRepeater(int id)
        {
            return await _encroachmentRegisterationRepository.RollBackEntryDetailsofEncroachmentRepeater(id);
        }

        public async Task<Zone> FetchSingleResultOnZoneList(int zoneid)
        {
            return await _encroachmentRegisterationRepository.FetchSingleResultOnZoneList(zoneid);
        }
    }
}