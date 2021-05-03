

using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IExtensionApprovalRepository : IGenericRepository<Extension>
    {
        Task<PagedResult<Extension>> GetPagedExtensionDetails(ExtensionApprovalSearchDto model, int userId);
        Task<Extension> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}