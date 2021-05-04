using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IModuleCategoryService : IEntityService<ModuleCategory>
    {
        Task<List<ModuleCategory>> GetAllModuleCategory();
        Task<List<ModuleCategory>> GetModuleCategoryUsingReport();

        Task<bool> Update(int id, ModuleCategory moduleCategory);
        Task<bool> Create(ModuleCategory moduleCategory);
        Task<ModuleCategory> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string moduleCategory);

        Task<PagedResult<ModuleCategory>> GetPagedModuleCategory(ModuleCategorySearchDto model);
    }
}
