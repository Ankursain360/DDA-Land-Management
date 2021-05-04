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

    public interface IDamagePayeeApprovalService : IEntityService<Damagepayeeregister>
    {
        Task<PagedResult<Damagepayeeregister>> GetPagedDamageForApproval(DamagepayeeRegisterApprovalDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<Damagepayeeregister> FetchSingleResult(int id);
        Task<List<Damagepayeeregister>> GetAllDamagePayeeApprovallist();
    }
}
