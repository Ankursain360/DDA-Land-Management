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
        int GetPreviousApprovalId(int proccessid, int serviceid);
        Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId);
        Task<List<Approvalproccess>> GetHistoryDetails(int proccessid, int id);
        int CheckIsApprovalStart(int proccessid, int serviceid);
    }
}
