using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDamagePayeeApprovalRepository : IGenericRepository<Damagepayeeregister>
    {
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagePayeeRegisterForApproval(DamagepayeeRegisterApprovalDto model, bool IsUser);
    }
}
