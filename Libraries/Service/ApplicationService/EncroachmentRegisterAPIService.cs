

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

        //public async Task<bool> Create(ApiSaveWatchandwardDto dto)
        //{
        //    Watchandward model = new Watchandward();
        //    model.RefNo = dto.RefNo;
        //    model.Date = dto.Date;
        //    model.PrimaryListNo = dto.PrimaryListNo;
        //    model.Landmark = dto.Landmark;
        //    model.Encroachment = dto.Encroachment;
        //    model.StatusOnGround = dto.StatusOnGround;
        //    model.IsActive = dto.IsActive;
        //    model.PhotoPath = dto.PhotoPath;
        //    model.Latitude = dto.Latitude;
        //    model.Longitude = dto.Longitude;
        //    model.Remarks = dto.Remarks;
        //    model.ApprovalZoneId = dto.ApprovalZoneId;
        //    model.IsActive = 1;
        //    model.CreatedBy = 1;
        //    model.CreatedDate = DateTime.Now;
        //    _watchWardAPIRepository.Add(model);
        //    var result = await _unitOfWork.CommitAsync() > 0;
        //    dto.Id = model.Id;
        //    return result;
        //}



      
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
