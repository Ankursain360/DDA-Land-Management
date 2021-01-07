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
            model.name = model.name == null ? string.Empty : model.name.Trim();
            model.module = model.module == null ? string.Empty : model.module.Trim();
            var data = await _dbContext.WorkflowTemplate.Include(x => x.Module)
              .Where(x => x.Name.Contains(model.name))
              .Where(x=>x.Module.Name.Contains(model.module))
                         
                             
                                 .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("MODULE"):
                        data.Results = data.Results.OrderBy(x => x.ModuleId).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;


                }

            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("MODULE"):
                        data.Results = data.Results.OrderByDescending(x => x.ModuleId).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;


                }
            }
            return data;
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
