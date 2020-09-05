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

        //Task<bool> Update(int id, Designation designation);
    }
}
