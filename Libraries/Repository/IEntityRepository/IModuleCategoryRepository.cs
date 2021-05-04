using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IModuleCategoryRepository : IGenericRepository<ModuleCategory>
    {
        Task<List<ModuleCategory>> GetModuleCategory();
        Task<bool> Any(int id, string name);
        Task<PagedResult<ModuleCategory>> GetPagedModuleCategory(ModuleCategorySearchDto model);

    }
}
