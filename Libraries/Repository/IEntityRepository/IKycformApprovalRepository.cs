

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IKycformApprovalRepository : IGenericRepository<Kycform>
    {
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version);
        Task<Kycform> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<PagedResult<Kycform>> GetPagedKycFormDetails(KycFormApprovalSearchDto model, int userId);

        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);

    }
}
