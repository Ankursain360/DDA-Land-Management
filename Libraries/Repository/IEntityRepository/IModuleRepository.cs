using Libraries.Repository.Common;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using Libraries.Model.Entity;

namespace Libraries.Repository.IEntityRepository
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<List<Module>> GetModule();
        Task<bool> Any(int id, string name);
    }

}
