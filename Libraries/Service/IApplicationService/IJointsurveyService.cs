using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IJointsurveyService
    {
        Task<List<Jointsurvey>> GetAllJointSurvey();

        Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Jointsurvey>> GetJointSurveyUsingRepo();
        Task<bool> Update(int id, Jointsurvey jointsurvey);
        Task<bool> Create(Jointsurvey jointsurvey);
        Task<Jointsurvey> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Jointsurvey>> GetPagedJointsurvey(JointSurveySearchDto model);
        Task<List<Jointsurveysitepositionmapped>> BindJointSiteMapped(int jointsurveyid);
        Task<bool> SaveSitePosition(List<Jointsurveysitepositionmapped> jointsurveysitepositionmappeds);
    }
}
