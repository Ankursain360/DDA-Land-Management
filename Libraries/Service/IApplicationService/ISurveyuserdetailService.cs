
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ISurveyuserdetailService : IEntityService<Surveyuserdetail>
    {
        Task<List<Surveyuserdetail>> GetAllSurveyuserdetail();
       
        Task<bool> Update(int id, Surveyuserdetail user);
        Task<bool> Create(Surveyuserdetail user);
        Task<Surveyuserdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Surveyuserrole>> GetUserDropDownList();

        // To check Unique Value  
        Task<bool> CheckUniqueuserName(int id, string username);
        Task<bool> CheckUniquePhone(int id, string phone);
        Task<bool> CheckUniqueEmail(int id, string email);
 
        Task<PagedResult<Surveyuserdetail>> GetPagedSurveyuserdetail(SurveyuserdetailSearchDto model);

    }
}
