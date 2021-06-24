using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IUserNotificationService : IEntityService<Usernotification>
    {

        Task<bool> Update(int id,  int userId); // To Upadte Particular data added by renu
        Task<bool> Create(Usernotification usernotification, int userId); // To Create Particular data added by renu
        int GetPreviousApprovalId(string proccessguid, int serviceid);
        Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id);
        int CheckIsApprovalStart(string proccessguid, int serviceid);
        Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions);
        Task<Approvalstatus> FetchSingleApprovalStatus(int id);
        Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode);
        Task<bool> RollBackEntry(string processguid, int serviceid);
        Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid);
        Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level);
        Task<List<UserNotificationAlertDto>> GetUserNotficationAlert(int userId);
        Task<List<UserNotificationAlertDto>> GetUserNotficationAlertAll(int userId);
    }
}
