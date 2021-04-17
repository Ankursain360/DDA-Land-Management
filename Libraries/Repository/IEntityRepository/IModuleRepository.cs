using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model);
        Task<List<Module>> GetAllModule();
        Task<bool> Any(int id, string name);
        Task<Module> GetModuleByGuid(string guid);
        Task<List<Menuactionrolemap>> ModuleFromMenuRoleActionMap(int roleId);
    }
}
