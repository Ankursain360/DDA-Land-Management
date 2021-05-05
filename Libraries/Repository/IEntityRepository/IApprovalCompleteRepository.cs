using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IEntityRepository
{
    public interface IApprovalCompleteRepository : IGenericRepository<Approvalproccess>
 
    {
        Task<List<ApprovalCompleteListDataDto>> GetApprovalCompleteModule(ApprovalCompleteSearchDto model);
        Task<List<ApprovalCompleteListDataDto>> BindModuleName();
        Task<Approvalurltemplatemapping> SingleResultProcessGuidBasisFromMapping(string processguid);
    }
}
