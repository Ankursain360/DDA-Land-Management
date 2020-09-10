using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IRoleService : IEntityService<Role>
    {
        Task<List<Role>> GetAllRole(); 
        Task<List<Zone>> GetAllZone(); 
        Task<List<Role>> GetRoleUsingRepo();
        Task<bool> Update(int id, Role role); 
        Task<bool> Create(Role Role);
        Task<Role> FetchSingleResult(int id);  
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string name);   
    }
}
