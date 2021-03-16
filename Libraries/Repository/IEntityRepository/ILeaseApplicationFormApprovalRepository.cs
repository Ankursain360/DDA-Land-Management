using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeaseApplicationFormApprovalRepository : IGenericRepository<Leaseapplication>
    {
        Task<PagedResult<Leaseapplication>> GetPagedLeaseApplicationFormDetails(LeaseApplicationFormApprovalSearchDto model, int userId);
        Task<Leaseapplication> FetchSingleResult(int id);
    }
}