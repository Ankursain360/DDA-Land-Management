using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IApprovalProccessRepository : IGenericRepository<Approvalproccess>
    {
        int GetPreviousApprovalId(string proccessguid, int serviceid);
        Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id);
        int CheckIsApprovalStart(string proccessguid, int serviceid);
        Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions);
        Task<Approvalstatus> FetchSingleApprovalStatus(int id);
        Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode);
        Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid);
        Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level);
        Task<ApplicationNotificationTemplate> FetchSingleNotificationTemplate(string guid);
    }
}