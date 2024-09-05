using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPermissionsRepository : IGenericRepository<Menuactionrolemap>
    {
        Task<List<Menuactionrolemap>> GetPermission(string moduleId, int roleId);
        Task<List<Module>> GetModuleList();
        Task<List<Menu>> GetMappedMenuWithAction(int moduleId);
        Task<List<Menuactionrolemap>> MenuactionrolemapList(int ModuleId, int RoleId);
        Task<bool> AuthorizeUser(string actionName, int roleId, int moduleId, int menuId);
    }
}