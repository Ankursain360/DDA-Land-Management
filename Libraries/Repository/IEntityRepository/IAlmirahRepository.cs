using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAlmirahRepository : IGenericRepository<Almirah>
    {
        Task<List<Almirah>> GetAlmirah();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Almirah>> GetPagedAlmirah(AlmirahSearchDto model);

    }
}
