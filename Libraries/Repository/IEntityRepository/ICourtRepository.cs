using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ICourtRepository : IGenericRepository<Court>
    {
        Task<PagedResult<Court>> GetPagedCourt(CourtSearchDto model);

        Task<List<Court>> GetAllCourt();
    }
}
