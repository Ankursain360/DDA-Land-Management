

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

    public interface IKycformApprovalService : IEntityService<Kycform>
    {

        Task<PagedResult<Kycform>> GetPagedKycFormDetails(KycFormApprovalSearchDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version);
        Task<Kycform> FetchSingleResult(int id);

        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);


    }
}
