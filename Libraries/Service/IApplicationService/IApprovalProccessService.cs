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
        int GetPreviouskycApprovalId(string proccessguid, int serviceid);//added by ishu 22/7/2021
        Task<Kycapprovalproccess> FetchKYCApprovalProcessDocumentDetails(int id);//added by ishu 22/7/2021
        Task<Kycapprovalproccess> FirstkycApprovalProcessData(string processguid, int serviceid); //added by ishu 22/7/2021
        Task<bool> UpdatePreviouskycApprovalProccess(int previousApprovalId, Kycapprovalproccess approvalproccess, int userId); // added by ishu 22/27/2021
        Task<Kycapprovalproccess> CheckLastKycUserForRevert(string processguid, int serviceid, int level);//added by ishu 23/7/2021
        Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId);
        Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id);
        int CheckIsApprovalStart(string proccessguid, int serviceid);
        Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions);
        Task<Approvalstatus> FetchSingleApprovalStatus(int id);
        Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode);
        Task<bool> RollBackEntry(string processguid, int serviceid);
        Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid);
        Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level);
        Task<ApplicationNotificationTemplate> FetchSingleNotificationTemplate(string guid);
    }
}
