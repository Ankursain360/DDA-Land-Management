using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface ILeaseApplicationFormApprovalService : IEntityService<Leaseapplication>
    {
        Task<PagedResult<Leaseapplication>> GetPagedLeaseApplicationFormDetails(LeaseApplicationFormApprovalSearchDto model, int userId, int id);
        Task<Leaseapplication> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
