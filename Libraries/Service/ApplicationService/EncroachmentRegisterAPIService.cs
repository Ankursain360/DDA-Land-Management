

using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Model.Entity;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class EncroachmentRegisterAPIService : EntityService<EncroachmentRegisteration>, IEncroachmentRegisterAPIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncroachmentRegisterAPIRepository _encroachmentRegisterAPIRepository;

        public EncroachmentRegisterAPIService(IUnitOfWork unitOfWork, IEncroachmentRegisterAPIRepository encroachmentRegisterAPIRepository) :
            base(unitOfWork, encroachmentRegisterAPIRepository)
        {
            _unitOfWork = unitOfWork;
            _encroachmentRegisterAPIRepository = encroachmentRegisterAPIRepository;
        }

    

        public async Task<bool> Create(ApiSaveEncroachmentRegisterDto dto)
        {
            EncroachmentRegisteration model = new EncroachmentRegisteration();
            model.WatchWardId = dto.WatchWardId;
            model.RefNo = dto.RefNo;
            model.DepartmentId = dto.DepartmentId;
            model.ZoneId = dto.ZoneId;
            model.DivisionId = dto.DivisionId;
            model.LocalityId = dto.LocalityId;
            model.EncrochmentDate = dto.EncrochmentDate;
            model.KhasraNo = dto.KhasraNo;
            model.AreaUnit = dto.AreaUnit;
            model.TotalAreaInBighaInspection = dto.TotalAreaInBighaInspection;
            model.TotalAreaInBiswaInspection = dto.TotalAreaInBiswaInspection;
            model.TotalAreaInBiswaniInspection = dto.TotalAreaInBiswaniInspection;
            model.TotalAreaInSqAcreHt = dto.TotalAreaInSqAcreHt;
            model.Area = dto.Area;
            model.LocationAddressWithLandMark = dto.LocationAddressWithLandMark;
            model.EncroacherName = dto.EncroacherName;
            model.StatusOfLand = dto.StatusOfLand;
            model.IsPossession = dto.IsPossession;
            model.PossessionType = dto.PossessionType;
            model.OtherDepartment = dto.OtherDepartment;
            model.PoliceStation = dto.PoliceStation;
            model.SecurityGuardOnDuty = dto.SecurityGuardOnDuty;
            model.IsEncroachment = dto.IsEncroachment;
            model.Remarks = dto.Remarks;
            model.ApprovalZoneId = dto.ApprovalZoneId;
            model.IsActive = 1;
            model.CreatedBy = 1;
            model.CreatedDate = DateTime.Now;
            _encroachmentRegisterAPIRepository.Add(model);
            var result = await _unitOfWork.CommitAsync() > 0;
            dto.Id = model.Id;
            return result;
        }


        public async Task<bool> UpdateBeforeApproval(ApiSaveEncroachmentRegisterDto dto)
        {
            var result = await _encroachmentRegisterAPIRepository.FindBy(a => a.Id == dto.Id);

            EncroachmentRegisteration model = result.FirstOrDefault();
            model.ApprovedStatus = dto.ApprovedStatus;
            model.PendingAt = dto.PendingAt;
            model.ModifiedBy = 1;
            model.ModifiedDate = DateTime.Now;
            _encroachmentRegisterAPIRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Zone> GetZonecode(int? zoneId)
        {
             Zone code = await _encroachmentRegisterAPIRepository.GetZonecode(zoneId);
            return code;
        }
        public async Task<List<APIGetDepartmentListDto>> GetDepartmentDropDownList()
        {
            List<APIGetDepartmentListDto> DepartmentList = await _encroachmentRegisterAPIRepository.GetDepartmentDropDownList();
            return DepartmentList;
        }
        public async Task<List<ApiGetZoneListDto>> GetZoneDropDownList(int departmentId)
        {
            List<ApiGetZoneListDto> List = await _encroachmentRegisterAPIRepository.GetZoneDropDownList(departmentId);
            return List;
        }
        public async Task<List<ApiGetDivisionListDto>> GetDivisionDropDownList(int zoneId)
        {
            List<ApiGetDivisionListDto> List = await _encroachmentRegisterAPIRepository.GetDivisionDropDownList(zoneId);
            return List;
        }
        public async Task<List<ApiGetLocalityListDto>> GetLocalityDropDownList(int divisionId)
        {
            List<ApiGetLocalityListDto> List = await _encroachmentRegisterAPIRepository.GetLocalityDropDownList(divisionId);
            return List;
        }
        

        public async Task<List<APIGetKhasraListDto>> GetKhasraDropDownList()
        {
            List<APIGetKhasraListDto> List = await _encroachmentRegisterAPIRepository.GetKhasraDropDownList();
            return List;
        }
    }
}
