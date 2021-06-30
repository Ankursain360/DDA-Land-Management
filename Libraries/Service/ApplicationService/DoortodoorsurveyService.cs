using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class DoortodoorsurveyService : EntityService<Doortodoorsurvey>, IDoortodoorsurveyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoortodoorsurveyRepository _doortodoorsurveyRepository;
        public DoortodoorsurveyService(IUnitOfWork unitOfWork, IDoortodoorsurveyRepository doortodoorsurveyRepository)
      : base(unitOfWork, doortodoorsurveyRepository)
        {
            _unitOfWork = unitOfWork;
            _doortodoorsurveyRepository = doortodoorsurveyRepository;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _doortodoorsurveyRepository.FindBy(a => a.Id == id);
            Doortodoorsurvey model = form.FirstOrDefault();
            model.IsActive = 0;
            _doortodoorsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Doortodoorsurvey> FetchSingleResult(int id)
        {
            return await _doortodoorsurveyRepository.FetchSingleResult(id);
        }




        public async Task<List<Presentuse>> GetAllPresentuse()
        {
            List<Presentuse> presentuseList = await _doortodoorsurveyRepository.GetAllPresentuse();
            return presentuseList;
        }
        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurveyUsingRepo()
        {
            return await _doortodoorsurveyRepository.GetDoortodoorsurvey();
        }

        public async Task<bool> Update(int id, Doortodoorsurvey doortodoorsurvey)
        {
            var result = await _doortodoorsurveyRepository.FindBy(a => a.Id == id);
            Doortodoorsurvey model = result.FirstOrDefault();
            model.PropertyAddress = doortodoorsurvey.PropertyAddress;
            model.GeoReferencingLattitude = doortodoorsurvey.GeoReferencingLattitude;
            model.Longitude = doortodoorsurvey.Longitude;
            model.PresentUseId = doortodoorsurvey.PresentUseId;
            model.ApproxPropertyArea = doortodoorsurvey.ApproxPropertyArea;
            model.NumberOfFloors = doortodoorsurvey.NumberOfFloors;
            model.CaelectricityNo = doortodoorsurvey.CaelectricityNo;
            model.IsActive = doortodoorsurvey.IsActive;
            model.KwaterNo = doortodoorsurvey.KwaterNo;
            model.PropertyHouseTaxNo = doortodoorsurvey.PropertyHouseTaxNo;
            model.OccupantName = doortodoorsurvey.OccupantName;
            model.Email = doortodoorsurvey.Email;
            model.IsActive = 1;
            model.Remarks = doortodoorsurvey.Remarks;
            model.MobileNo = doortodoorsurvey.MobileNo;
            model.OccupantAadharNo = doortodoorsurvey.OccupantAadharNo;
            model.VoterIdNo = doortodoorsurvey.VoterIdNo;
            model.ModifiedBy = doortodoorsurvey.ModifiedBy;
            _doortodoorsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Doortodoorsurvey doortodoorsurvey)
        {
            doortodoorsurvey.IsActive = 1;
            _doortodoorsurveyRepository.Add(doortodoorsurvey);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model)
        {
            return await _doortodoorsurveyRepository.GetPagedDoortodoorsurvey(model);
        }


        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurvey()
        {

            return await _doortodoorsurveyRepository.GetDoortodoorsurvey();
        }

        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurveyReport(DoorToDoorSurveyReportSearchDto model)
        {
            return await _doortodoorsurveyRepository.GetPagedDoortodoorsurveyReport(model);
        }
        public async Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item)
        {
            item.CreatedDate = DateTime.Now;
            return await _doortodoorsurveyRepository.SaveDoorToDoorSurveyIdentityProofs(item);
        }

        public async Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item)
        {
            item.CreatedDate = DateTime.Now;
            return await _doortodoorsurveyRepository.SaveDoorToDoorSurveyPropertyProofs(item);
        }

        public async Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id)
        {
            return await _doortodoorsurveyRepository.DeleteDoorToDoorSurveyIdentityProofs(id);
        }

        public async Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id)
        {
            return await _doortodoorsurveyRepository.DeleteDoorToDoorSurveyPropertyProofs(id);
        }

        public async Task<Doortodoorsurveyidentityproof> FetchSingleResultDoor2DoorSurveyIdentity(int id)
        {
            return await _doortodoorsurveyRepository.FetchSingleResultDoor2DoorSurveyIdentity(id);
        }

        public async Task<Doortodoorsurveypropertyproof> FetchSingleResultDoor2DoorSurveyProperty(int id)
        {
            return await _doortodoorsurveyRepository.FetchSingleResultDoor2DoorSurveyProperty(id);
        }
    }
}
