using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IProccessWorkflowRepository : IGenericRepository<Processworkflow>
    {
        int GetPreviousApprovalId(string proccessguid, int serviceid);
        Task<List<Approvalproccess>> GetHistoryDetails(string proccessguid, int id);
        int FetchCountResultForProccessWorkflow(int workflowTemplateId);
    }
}