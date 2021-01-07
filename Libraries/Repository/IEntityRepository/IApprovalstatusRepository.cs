using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
  public  interface IApprovalstatusRepository : IGenericRepository<Approvalstatus>
    {
      
        Task<PagedResult<Approvalstatus>> GetPagedApprovalStatus(ApprovalstatusSearchDto model);
        Task<List<Approvalstatus>> GetAllApprovedstatus();

    }
}
