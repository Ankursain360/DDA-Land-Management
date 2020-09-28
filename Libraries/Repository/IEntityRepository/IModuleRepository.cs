using Libraries.Repository.Common;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model);
        Task<List<Module>> GetModule();
        Task<bool> Any(int id, string name);
    }

}
