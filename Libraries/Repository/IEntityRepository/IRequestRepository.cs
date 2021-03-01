using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<List<Request>> GetAllRequest();
        Task<PagedResult<Request>> GetPagedRequest(RequestSearchDto model);

    }
}
