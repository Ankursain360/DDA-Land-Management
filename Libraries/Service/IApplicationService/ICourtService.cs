using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ICourtService : IEntityService<Court>
    {

        Task<List<Court>> GetAllCourt();
        Task<bool> Create(Court court);

        Task<PagedResult<Court>> GetPagedCourt(CourtSearchDto model);

        Task<bool> Update(int id, Court court);
        Task<Court> FetchSingleResult(int id);

    }
}
