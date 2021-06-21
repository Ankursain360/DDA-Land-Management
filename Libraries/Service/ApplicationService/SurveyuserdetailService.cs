
using Dto.Search;
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

    public class SurveyuserdetailService : EntityService<Surveyuserdetail>, ISurveyuserdetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyuserdetailRepository _surveyuserdetailRepository;

        public SurveyuserdetailService(IUnitOfWork unitOfWork, ISurveyuserdetailRepository surveyuserdetailRepository)
        : base(unitOfWork, surveyuserdetailRepository)
        {
            _unitOfWork = unitOfWork;
            _surveyuserdetailRepository = surveyuserdetailRepository;
        }

        public async Task<List<Surveyuserdetail>> GetAllSurveyuserdetail()
        {
            return await _surveyuserdetailRepository.GetAllSurveyuserdetail();
        }


        public async Task<Surveyuserdetail> FetchSingleResult(int id)
        {
            var result = await _surveyuserdetailRepository.FindBy(a => a.Id == id);
            Surveyuserdetail model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Surveyuserdetail user)
        {
            var result = await _surveyuserdetailRepository.FindBy(a => a.Id == id);
            Surveyuserdetail model = result.FirstOrDefault();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.PhoneNo = user.PhoneNo;
            model.EmailId = user.EmailId;
            model.Password = user.Password;
            model.RoleId = user.RoleId;
            model.IsActive = user.IsActive;
           
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _surveyuserdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Surveyuserdetail user)
        {
            user.CreatedBy = 1;
            user.CreatedDate = DateTime.Now;
            _surveyuserdetailRepository.Add(user);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueuserName(int id, string username)
        {
            bool result = await _surveyuserdetailRepository.AnyUser(id, username);
            return result;
        }
        public async Task<bool> CheckUniquePhone(int id, string phone)
        {
            bool result = await _surveyuserdetailRepository.anyPhone(id,phone);
            return result;
        }

        public async Task<bool> CheckUniqueEmail(int id, string email)
        {
            bool result = await _surveyuserdetailRepository.anyEmail(id,email);
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _surveyuserdetailRepository.FindBy(a => a.Id == id);
            Surveyuserdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _surveyuserdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Surveyuserdetail>> GetPagedSurveyuserdetail(SurveyuserdetailSearchDto model)
        {
            return await _surveyuserdetailRepository.GetPagedSurveyuserdetail(model);
        }

        public async Task<List<Surveyuserrole>> GetUserDropDownList()
        {
            List<Surveyuserrole> List = await _surveyuserdetailRepository.GetUserDropDownList();
            return List;
        }


    }
}
