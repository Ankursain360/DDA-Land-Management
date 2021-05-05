using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IApprovalCompleteService : IEntityService<Approvalproccess>
    {
        Task<List<ApprovalCompleteListDataDto>> GetApprovalCompleteModule(ApprovalCompleteSearchDto model);
        Task<List<ApprovalCompleteListDataDto>> BindModuleName();
        Task<Approvalurltemplatemapping> SingleResultProcessGuidBasisFromMapping(string processguid);
    }
}
