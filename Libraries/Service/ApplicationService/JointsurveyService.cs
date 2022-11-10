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
    public class JointsurveyService : EntityService<Jointsurvey>, IJointsurveyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IJointsurveyRepository _JointsurveyRepository;
        public JointsurveyService(IUnitOfWork unitOfWork, IJointsurveyRepository jointsurveyRepository)
: base(unitOfWork, jointsurveyRepository)
        {
            _unitOfWork = unitOfWork;
            _JointsurveyRepository = jointsurveyRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _JointsurveyRepository.FindBy(a => a.Id == id);
            Jointsurvey model = form.FirstOrDefault();
            model.IsActive = 0;
            _JointsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Jointsurvey> FetchSingleResult(int id)
        {
            var result = await _JointsurveyRepository.FindBy(a => a.Id == id);
            Jointsurvey model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Khasra>> BindKhasra(int villageid)
        {
            List<Khasra> khasraList = await _JointsurveyRepository.BindKhasra(villageid);
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _JointsurveyRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Jointsurvey>> GetAllJointSurvey()
        {

            return await _JointsurveyRepository.GetAllJointSurvey();
        }

        public async Task<List<Jointsurvey>> GetAllJointsurveyList(JointSurveySearchDto model)
        {
            return await _JointsurveyRepository.GetAllJointsurveyList(model);
        }

        public async Task<List<Jointsurvey>> GetJointSurveyUsingRepo()
        {
            return await _JointsurveyRepository.GetAllJointSurvey();
        }

        public async Task<bool> Update(int id, Jointsurvey jointsurvey)
        {
            var result = await _JointsurveyRepository.FindBy(a => a.Id == id);
            Jointsurvey model = result.FirstOrDefault();
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
            _JointsurveyRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Jointsurvey jointsurvey)
        {
            //Jointsurvey model = new Jointsurvey();
            //model.VillageId = jointsurvey.VillageId;
            //model.KhasraId = jointsurvey.KhasraId;
            //model.AreaInBigha = jointsurvey.AreaInBigha;
            //model.AreaInBiswa = jointsurvey.AreaInBiswa;
            //model.Remarks = jointsurvey.Remarks;
            //model.NatureOfStructure = jointsurvey.NatureOfStructure;
            //model.JointSurveyDate = jointsurvey.JointSurveyDate;
            jointsurvey.CreatedBy = jointsurvey.CreatedBy;
            jointsurvey.CreatedDate = DateTime.Now;
            jointsurvey.IsActive = 1;

            _JointsurveyRepository.Add(jointsurvey);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Jointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model)
        {
            return await _JointsurveyRepository.GetPagedJointsurvey(model);
        }
    }
}
