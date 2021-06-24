using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class Door2DoorAPIService : EntityService<Doortodoorsurvey>, IDoor2DoorAPIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoor2DoorAPIRepository _door2DoorAPIRepository;

        public Door2DoorAPIService(IUnitOfWork unitOfWork, IDoor2DoorAPIRepository door2DoorAPIRepository) :
            base(unitOfWork, door2DoorAPIRepository)
        {
            _unitOfWork = unitOfWork;
            _door2DoorAPIRepository = door2DoorAPIRepository;
        }

        public async Task<bool> Create(ApiSaveDoor2DoorSurveyDto dto)
        {
            Doortodoorsurvey model = new Doortodoorsurvey();
            model.PropertyAddress = dto.PropertyAddress;
            model.GeoReferencingLattitude = dto.GeoReferencingLattitude;
            model.Longitude = dto.Longitude;
            model.PresentUseId = dto.PresentUseId;
            model.ApproxPropertyArea = dto.ApproxPropertyArea;
            model.NumberOfFloors = dto.NumberOfFloors;
            model.CaelectricityNo = dto.CaelectricityNo;
            model.IsActive = dto.IsActive;
            model.KwaterNo = dto.KwaterNo;
            model.PropertyHouseTaxNo = dto.PropertyHouseTaxNo;
            model.OccupantName = dto.OccupantName;
            model.Email = dto.Email;
            model.IsActive = 1;
            model.Remarks = dto.Remarks;
            model.MobileNo = dto.MobileNo;
            model.OccupantAadharNo = dto.OccupantAadharNo;
            model.VoterIdNo = dto.VoterIdNo;
            model.OccupantIdentityPrrofFilePath = dto.OccupantIdentityPrrofFilePath;
            model.PropertyFilePath = dto.PropertyFilePath;
            model.Remarks = dto.Remarks;
            model.CreatedBy = dto.CreatedBy;
            model.CreatedDate = DateTime.Now;
            _door2DoorAPIRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto)
        {
            return await _door2DoorAPIRepository.GetSurveyDetails(dto);
        }
    }
}
