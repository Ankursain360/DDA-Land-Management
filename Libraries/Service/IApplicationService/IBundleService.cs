using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IBundleService : IEntityService<Bundle>
    {
        Task<List<Bundle>> GetAllBundle();
        Task<List<Bundle>> GetBundleUsingReport();

        Task<bool> Update(int id, Bundle bundle);
        Task<bool> Create(Bundle bundle);
        Task<Bundle> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string bundle);

        Task<PagedResult<Bundle>> GetPagedBundle(BundleSearchDto model);
    }
}
