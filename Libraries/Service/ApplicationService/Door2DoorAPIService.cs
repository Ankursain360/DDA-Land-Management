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
            model.Remarks = dto.Remarks;
            model.CreatedBy = dto.CreatedBy;
            model.CreatedDate = DateTime.Now;
            _door2DoorAPIRepository.Add(model); 
            var result = await _unitOfWork.CommitAsync() > 0;
            dto.Id =  model.Id;
            return result;
        }


        public async Task<bool> Update(ApiSaveDoor2DoorSurveyDto dto)
        {
            var result = await _door2DoorAPIRepository.FindBy(a => a.Id == dto.Id);
            Doortodoorsurvey model = result.FirstOrDefault();
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
            model.Remarks = dto.Remarks;
            model.ModifiedBy = dto.CreatedBy;
            model.ModifiedDate = DateTime.Now;
            _door2DoorAPIRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiGetAllDoor2DoorSurveyParamsDto dto, int adminroleid)
        {
            return await _door2DoorAPIRepository.GetAllSurveyDetails(dto, adminroleid);
        }

        public async Task<List<ApiGetPresentUseDto>> GetPresentUseDetails()
        {
            return await _door2DoorAPIRepository.GetPresentUseDetails();
        }

        public async Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto, string identityDocumentPath, string propertyDocumentPath)
        {
            return await _door2DoorAPIRepository.GetSurveyDetails(dto, identityDocumentPath, propertyDocumentPath);
        }

        public async Task<List<ApiSurveyUserDetailsDto>> VerifySurveyUserDetailsLogin(ApiSurveyUserLoginDto dto)
        {
            return await _door2DoorAPIRepository.VerifySurveyUserDetailsLogin(dto);
        }

        public async Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item)
        {
            item.CreatedDate = DateTime.Now;
            return await _door2DoorAPIRepository.SaveDoorToDoorSurveyIdentityProofs(item);
        }

        public async Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item)
        {
            item.CreatedDate = DateTime.Now;
            return await _door2DoorAPIRepository.SaveDoorToDoorSurveyPropertyProofs(item);
        }

        public async Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id)
        {
            return await _door2DoorAPIRepository.DeleteDoorToDoorSurveyIdentityProofs(id);
        }

        public async Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id)
        {
            return await _door2DoorAPIRepository.DeleteDoorToDoorSurveyPropertyProofs(id);
        }
    }
}
