using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository.EntityRepository
{
    public class ModuleCategoryRespository : GenericRepository<ModuleCategory>, IModuleCategoryRepository
    {
        public ModuleCategoryRespository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<ModuleCategory>> GetModuleCategory()
        {
            return await _dbContext.ModuleCategory.Where(x => x.IsActive == 1).ToListAsync();
        }

      

        public async Task<PagedResult<ModuleCategory>> GetPagedModuleCategory(ModuleCategorySearchDto model)
        {
            var data = await _dbContext.ModuleCategory
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.CategoryName.Contains(model.name)))
                  .GetPaged<ModuleCategory>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.ModuleCategory
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.CategoryName.Contains(model.name)))
                           .OrderBy(s => s.CategoryName)
                           .GetPaged<ModuleCategory>(model.PageNumber, model.PageSize);

                        break;                  
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.ModuleCategory
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.CategoryName.Contains(model.name)))
                            .OrderBy(s => s.CategoryName)
                            .GetPaged<ModuleCategory>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.ModuleCategory
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.CategoryName.Contains(model.name)))
                           .OrderByDescending(s => s.CategoryName)
                           .GetPaged<ModuleCategory>(model.PageNumber, model.PageSize);
                        break;
                   

                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.ModuleCategory
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.CategoryName.Contains(model.name)))
                           .OrderByDescending(s => s.CategoryName)
                           .GetPaged<ModuleCategory>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.ModuleCategory.AnyAsync(t => t.Id != id && t.CategoryName.ToLower() == name.ToLower());
        }
    }
}
