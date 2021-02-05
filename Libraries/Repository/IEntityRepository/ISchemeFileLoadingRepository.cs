using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface ISchemeFileLoadingRepository : IGenericRepository<Schemefileloading>
    {
        Task<List<Schemefileloading>> GetSchemeFileloading();
        Task<bool> Any(int id, string schemename);
        Task<PagedResult<Schemefileloading>> GetPagedSchemeFileLoading(SchemeFileLoadingSearchDto model);
    }
}