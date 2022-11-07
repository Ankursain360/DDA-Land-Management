using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IRequestApprovalProcessRepository : IGenericRepository<Request>
    {
        Task<PagedResult<Request>> GetPagedProcessRequest(RequestApprovalSearchDto model, int userId);
        Task<Request> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<List<Request>> GetAllRequest();
        Task<List<Request>> GetAllProcessRequestList(RequestApprovalSearchDto model, int userId);
    }
}
