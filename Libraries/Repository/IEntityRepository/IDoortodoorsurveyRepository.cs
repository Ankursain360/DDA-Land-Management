using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDoortodoorsurveyRepository : IGenericRepository<Doortodoorsurvey>
    {
        Task<List<Doortodoorsurvey>> GetDoortodoorsurvey();
        Task<List<Presentuse>> GetAllPresentuse();
        Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model);

        Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurveyReport(DoorToDoorSurveyReportSearchDto model);

    }
}
