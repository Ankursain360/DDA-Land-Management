using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IRequestApprovalProcessService : IEntityService<Request>
    {
        Task<Request> FetchSingleResult(int id);
        Task<PagedResult<Request>> GetPagedProcessRequest(RequestApprovalSearchDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<List<Request>> GetAllRequest();
        Task<List<Request>> GetAllProcessRequestList(RequestApprovalSearchDto model, int userId);
    }
}
