
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ISurveyuserdetailRepository : IGenericRepository<Surveyuserdetail>
    {

        Task<bool> AnyUser(int id, string username);
        Task<bool> anyPhone(int id, string phone);
        Task<bool> anyEmail(int id, string email);


        Task<List<Surveyuserrole>> GetUserDropDownList();
        Task<List<Surveyuserdetail>> GetAllSurveyuserdetail();
        Task<PagedResult<Surveyuserdetail>> GetPagedSurveyuserdetail(SurveyuserdetailSearchDto model);
    }
}
