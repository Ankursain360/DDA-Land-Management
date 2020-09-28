using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
   
    public interface IModuleService : IEntityService<Module>
    {
        Task<List<Module>> GetAllModule();
        Task<List<Module>> GetModuleUsingRepo();

        Task<bool> Update(int id, Module module); 

        Task<bool> Create(Module module);

        Task<Module> FetchSingleResult(int id);  

        Task<bool> Delete(int id);   

        Task<bool> CheckUniqueName(int id, string Module);   // To check Unique Value  
        Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model);
    }
}
