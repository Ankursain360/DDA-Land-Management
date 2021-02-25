using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandjointsurveyRepository : IGenericRepository<Newlandjointsurvey>
    {

        Task<List<Newlandjointsurvey>> GetAllJointSurvey();

        Task<List<Zone>> GetAllZone();
        Task<List<Newlandvillage>> GetAllVillage(int zoneId);
        Task<List<Newlandkhasra>> GetAllKhasra(int villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int khasraId);
        //Task<PagedResult<Newlandjointsurvey>> GetPagedJointsurvey(NewlandJointSurveySearchDto model);

    }
}
