using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;



namespace Libraries.Service.IApplicationService
{
    public interface IRequestService : IEntityService <Request>
    {
        Task<bool> Create(Request request);
        Task<bool> Update(int id, Request request);
        Task<List<Request>> GetAllRequest();
        Task<Request> FetchSingleResult(int id);
        Task<List<Request>> GetRequestUsingRepo();
        Task<bool> Delete(int id);
        Task<PagedResult<Request>> GetPagedRequest(RequestSearchDto model);
        Task<bool> UpdateBeforeApproval(int id, Request request);
    }
}
