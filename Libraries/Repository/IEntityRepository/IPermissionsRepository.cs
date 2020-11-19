using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPermissionsRepository : IGenericRepository<Menuactionrolemap>
    {
        Task<List<Menuactionrolemap>> GetPermission(string moduleId, int roleId);
        Task<List<Module>> GetModuleList();
        Task<List<Menu>> GetMappedMenuWithAction(int moduleId);
    }
}