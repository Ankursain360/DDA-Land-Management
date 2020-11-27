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

        Task<bool> Update(int id, Approvalproccess approvalproccess); // To Upadte Particular data added by renu
        Task<bool> Create(Approvalproccess approvalproccess); // To Create Particular data added by renu
        int GetPreviousApprovalId(int proccessid, int serviceid);
        Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess);
    }
}
