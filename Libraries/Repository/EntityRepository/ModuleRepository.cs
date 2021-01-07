using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(DataContext dbContext) : base(dbContext)
        {

        }
        //public async Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model)
        //{
        //    return await _dbContext.Module.GetPaged<Module>(model.PageNumber, model.PageSize);
        //}

        public async Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model)
        {
            
            var data = await _dbContext.Module.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                   && (string.IsNullOrEmpty(model.description) || x.Description.Contains(model.description)))

                
                            
                  .OrderByDescending(s => s.IsActive)
                  .ThenBy(s => s.Name)
                  .GetPaged<Module>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("DESCRIPTION"):
                        data.Results = data.Results.OrderBy(x => x.Description).ToList();
                        break;
                    case ("URL"):
                        data.Results = data.Results.OrderBy(x => x.Url).ToList();
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
                    case ("DESCRIPTION"):
                        data.Results = data.Results.OrderByDescending(x => x.Description).ToList();
                        break;
                    case ("URL"):
                        data.Results = data.Results.OrderByDescending(x => x.Url).ToList();
                        break;
                   
                }
            }
            return data;
        }

        public async Task<List<Module>> GetModule()
        {
            return await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Module.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Module>> GetAllModule()
        {
            return await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<Module> GetModuleByGuid(string guid)
        {
            return await _dbContext.Module.FirstOrDefaultAsync(x => x.Guid == guid && x.IsActive == 1);
        }
    }
}