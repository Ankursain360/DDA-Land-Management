using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IProccessWorkflowService : IEntityService<Processworkflow>
    {

        //Task<bool> Update(int id, Approvalproccess approvalproccess, int userId); // To Upadte Particular data added by renu
        Task<bool> Create(Processworkflow proccess); // To Create Particular data added by renu
        int GetPreviousApprovalId(string proccessguid, int serviceid);
        //Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId);
        Task<List<Approvalproccess>> GetHistoryDetails(string proccessguid, int id);
        int FetchCountResultForProccessWorkflow(int workflowTemplateId);
    }
}
