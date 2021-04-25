using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IApprovalProccessService : IEntityService<Approvalproccess>
    {

        Task<bool> Update(int id, Approvalproccess approvalproccess, int userId); // To Upadte Particular data added by renu
        Task<bool> Create(Approvalproccess approvalproccess, int userId); // To Create Particular data added by renu
        int GetPreviousApprovalId(string proccessguid, int serviceid);
        Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId);
        Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id);
        int CheckIsApprovalStart(string proccessguid, int serviceid);
        Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions);
        Task<Approvalstatus> FetchSingleApprovalStatus(int id);
        Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode);
        Task<bool> RollBackEntry(string processguid, int serviceid);
        Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid);
    }
}
