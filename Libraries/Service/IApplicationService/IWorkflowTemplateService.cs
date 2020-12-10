using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IWorkflowTemplateService : IEntityService<WorkflowTemplate>
    {
        Task<List<WorkflowTemplate>> GetAllWorkflowTemplate(); // To Get all data added by renu

        Task<bool> Update(int id, WorkflowTemplate workflowtemplate); // To Upadte Particular data added by renu

        Task<bool> Create(WorkflowTemplate workflowtemplate);

        Task<WorkflowTemplate> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string workflowtemplate);   // To check Unique Value  for workflowtemplate

        Task<PagedResult<WorkflowTemplate>> GetPagedWorkflowTemplate(WorkflowTemplateSearchDto model);
        Task<List<Module>> GetAllModuleList();
        //Task<List<User>> GetUserlist();
        //Task<List<Role>> GetRolelist();
    }
}
