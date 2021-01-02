using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
namespace Libraries.Repository.IEntityRepository
{

    public interface IBundleRepository : IGenericRepository<Bundle>
    {
        Task<List<Bundle>> GetBundle();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Bundle>> GetPagedBundle(BundleSearchDto model);

    }
}
