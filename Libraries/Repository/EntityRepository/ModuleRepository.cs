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


        public async Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model)
        {

            var data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                           && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                          .GetPaged<Module>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                        .OrderByDescending(s => s.IsActive)
                                         .GetPaged<Module>(model.PageNumber, model.PageSize);
                }
                else if (model.SortBy.ToUpper() == "SORTBY")
                {
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                         .OrderBy(s => s.SortBy)
                                         .GetPaged<Module>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                           .OrderBy(s =>
                                           (model.SortBy.ToUpper() == "MODULECATEGORY" ? (s.ModuleCategory == null ? null : s.ModuleCategory.CategoryName)
                                           : model.SortBy.ToUpper() == "NAME" ? (s.Name)
                                           : model.SortBy.ToUpper() == "URL" ? (s.Url)
                                           : (s.ModuleCategory == null ? null : s.ModuleCategory.CategoryName))
                                           )
                                           .GetPaged<Module>(model.PageNumber, model.PageSize);
                }

            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                         .OrderBy(s => s.IsActive)
                                         .GetPaged<Module>(model.PageNumber, model.PageSize);
                }
                else if (model.SortBy.ToUpper() == "SORTBY")
                {
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                         .OrderByDescending(s => s.SortBy)
                                         .GetPaged<Module>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Module
                                          .Include(x => x.ModuleCategory)
                                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                           && (string.IsNullOrEmpty(model.url) || x.Url.Contains(model.url))
                                          && x.ModuleCategoryId == ((model.modulecategoryId == 0) ? x.ModuleCategoryId : model.modulecategoryId)
                                          )
                                           .OrderByDescending(s =>
                                           (model.SortBy.ToUpper() == "MODULECATEGORY" ? (s.ModuleCategory == null ? null : s.ModuleCategory.CategoryName)
                                           : model.SortBy.ToUpper() == "NAME" ? (s.Name)
                                           : model.SortBy.ToUpper() == "URL" ? (s.Url)
                                           : (s.ModuleCategory == null ? null : s.ModuleCategory.CategoryName))
                                           )
                                           .GetPaged<Module>(model.PageNumber, model.PageSize);
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

        public async Task<List<Menuactionrolemap>> ModuleFromMenuRoleActionMap(int roleId)
        {
            var Data = await _dbContext.Menuactionrolemap
                                       .Include(x => x.Module)
                                       .OrderBy(x => x.ModuleId)
                                       .Where(x => x.RoleId == roleId && x.Module.IsActive==1).ToListAsync();
            return (Data.GroupBy(x => x.ModuleId).SelectMany(g => g.OrderBy(d => d.ModuleId).Take(1)).ToList());
        }

        public async Task<List<ModuleCategory>> GetModuleCategory()
        {
            List<ModuleCategory> ModuleCategoryList = await _dbContext.ModuleCategory
                .Where(x => x.IsActive == 1).ToListAsync();
            return ModuleCategoryList;
        }

    }
}