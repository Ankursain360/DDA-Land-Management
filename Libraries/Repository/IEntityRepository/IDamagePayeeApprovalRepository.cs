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
        Task<PagedResult<Damagepayeeregister>> GetPagedDamageForApproval(DamagepayeeRegisterApprovalDto model, int userId);

        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<Damagepayeeregister> FetchSingleResult(int id);
    }
}
