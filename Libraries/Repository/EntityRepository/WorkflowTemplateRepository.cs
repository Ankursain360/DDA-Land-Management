using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class WorkflowTemplateRepository : GenericRepository<WorkflowTemplate>, IWorkflowTemplateRepository
    {

        public WorkflowTemplateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<WorkflowTemplate>> GetPagedWorkflowTemplate(WorkflowTemplateSearchDto model)
        {
            return await _dbContext.WorkflowTemplate.Include(x => x.Module).GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Designation.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Module>> GetAllModuleList()
        {
            return await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
        }

        //public async Task<List<Role>> GetRolelist()
        //{
        //    var result = await _dbContext.Role.Where(x => x.IsActive == 1).ToListAsync();
        //    return result;
        //}

        //public async Task<List<User>> GetUserlist()
        //{
        //    var result = await _dbContext.User.Where(x => x.IsActive == 1).ToListAsync();
        //    return result;
        //}
    }
}
