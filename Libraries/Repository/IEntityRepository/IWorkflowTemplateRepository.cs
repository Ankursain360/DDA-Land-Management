using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IWorkflowTemplateRepository : IGenericRepository<WorkflowTemplate>
    {
        Task<PagedResult<WorkflowTemplate>> GetPagedWorkflowTemplate(WorkflowTemplateSearchDto model);

        Task<bool> Any(int id, string name);
        Task<List<Module>> GetAllModuleList();
        Task<List<Role>> GetRolelist();
        Task<List<User>> GetUserlist();
    }
}