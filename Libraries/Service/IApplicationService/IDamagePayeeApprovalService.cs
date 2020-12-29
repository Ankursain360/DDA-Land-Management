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

    public interface IDamagePayeeApprovalService : IEntityService<Damagepayeeregistertemp>
    {
        Task<PagedResult<Damagepayeeregistertemp>> GetPagedDamagePayeeRegisterForApproval(DamagepayeeRegisterApprovalDto model, bool IsUser);
    }
}
