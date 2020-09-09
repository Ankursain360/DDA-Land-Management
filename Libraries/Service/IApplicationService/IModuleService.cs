using Libraries.Model.Entity;
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

        Task<bool> Update(int id, Module module); // To Upadte Particular data added by renu

        Task<bool> Create(Module module);

        Task<Module> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string Module);   // To check Unique Value  for designation
    }
}
