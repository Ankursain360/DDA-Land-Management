using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dto.Search;


namespace Libraries.Service.ApplicationService
{
    public class NewlandjointsurveyService : EntityService<Newlandjointsurvey>, INewlandjointsurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandjointsurveyRepository _newlandjointsurveyRepository;
        public NewlandjointsurveyService(IUnitOfWork unitOfWork, INewlandjointsurveyRepository newlandjointsurveyRepository)
   : base(unitOfWork, newlandjointsurveyRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandjointsurveyRepository = newlandjointsurveyRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _newlandjointsurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandjointsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandjointsurvey> FetchSingleResult(int id)
        {
            var result = await _newlandjointsurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> villageList1 = await _newlandjointsurveyRepository.GetAllZone();
            return villageList1;
        }


    
        public async Task<List<Newlandvillage>> GetAllVillage(int zoneId)
        {
            List<Newlandvillage> villageList = await _newlandjointsurveyRepository.GetAllVillage(zoneId);
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandjointsurveyRepository.GetAllKhasra(villageId);
            return khasraList;
        }
    


        public async Task<List<Newlandjointsurvey>> GetAllJointSurvey()
        {

            return await _newlandjointsurveyRepository.GetAllJointSurvey();
        }

        public async Task<Newlandkhasra> FetchSingleKhasraResult(int khasraId)
        {
            return await _newlandjointsurveyRepository.FetchSingleKhasraResult(khasraId);
        }


        public async Task<List<Newlandjointsurvey>> GetJointSurveyUsingRepo()
        {
            return await _newlandjointsurveyRepository.GetAllJointSurvey();
        }

        public async Task<bool> Update(int id, Newlandjointsurvey jointsurvey)
        {
            var result = await _newlandjointsurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = result.FirstOrDefault();
            model.SitePosition = jointsurvey.SitePosition;
            model.AreaInBigha = jointsurvey.AreaInBigha;
            model.AreaInBiswa = jointsurvey.AreaInBiswa;
            model.VillageId = jointsurvey.VillageId;
            model.KhasraId = jointsurvey.KhasraId;
            model.SitePosition = jointsurvey.SitePosition;
            model.Remarks = jointsurvey.Remarks;
            model.NatureOfStructure = jointsurvey.NatureOfStructure;
            model.JointSurveyDate = jointsurvey.JointSurveyDate;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            model.IsActive = jointsurvey.IsActive;
            _newlandjointsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandjointsurvey jointsurvey)
        {
            jointsurvey.CreatedBy = 1;
            jointsurvey.CreatedDate = DateTime.Now;
            jointsurvey.IsActive = 1;

            _newlandjointsurveyRepository.Add(jointsurvey);
            return await _unitOfWork.CommitAsync() > 0;
        }


        //public async Task<PagedResult<Newlandjointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model)
        //{
        //    return await _JointsurveyRepository.GetPagedJointsurvey(model);
        //}




    }
}
