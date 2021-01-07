using System;
using System.Collections.Generic;
using System.Text;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
  public  interface IApprovalstatusService : IEntityService<Approvalstatus>
    {
        Task<PagedResult<Approvalstatus>> GetPagedApprovalStatus(ApprovalstatusSearchDto model);
        Task<List<Approvalstatus>> GetAllApprovalstatus();
        Task<bool> Create(Approvalstatus approvalstatus);
        Task<bool> Update(int id, Approvalstatus approvalstatus);
        Task<Approvalstatus> FetchSingleResult(int id);

        Task<bool> Delete(int id);


    }
}
