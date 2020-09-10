using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<List<Role>> GetRole();
        Task<List<Zone>> GetAllZone();
        Task<bool> Any(int id, string name);
    }
}

