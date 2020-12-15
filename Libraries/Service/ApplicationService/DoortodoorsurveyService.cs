using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
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
            //model.isa = 0;
            _doortodoorsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Doortodoorsurvey> FetchSingleResult(int id)
        {
            var result = await _doortodoorsurveyRepository.FindBy(a => a.Id == id);
            Doortodoorsurvey model = result.FirstOrDefault();
            return model;
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
            model.MuncipalNo = doortodoorsurvey.MuncipalNo;
            model.GeoReferencing = doortodoorsurvey.GeoReferencing;
            model.PresentUseId = doortodoorsurvey.PresentUseId;
            model.ApproxPropertyArea = doortodoorsurvey.ApproxPropertyArea;
            model.NumberOfFloors = doortodoorsurvey.NumberOfFloors;
            model.CaelectricityNo = doortodoorsurvey.CaelectricityNo;

            model.KwaterNo = doortodoorsurvey.KwaterNo;
            model.PropertyHouseTaxNo = doortodoorsurvey.PropertyHouseTaxNo;
            model.OccupantName = doortodoorsurvey.OccupantName;
            model.Address = doortodoorsurvey.Address;
            model.Email = doortodoorsurvey.Email;

            // model.ModifiedDate = datetime.Now;
            model.ModifiedBy = 1;
            _doortodoorsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Doortodoorsurvey doortodoorsurvey)
        {
            doortodoorsurvey.CreatedBy = 1;
            //  doortodoorsurvey.CreatedDate = DateTime.Now;

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








    }
}
