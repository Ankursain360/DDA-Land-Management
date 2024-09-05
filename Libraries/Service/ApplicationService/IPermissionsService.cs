using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Component;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IPermissionsService : IEntityService<Menuactionrolemap>
    {
        Task<List<MenuDetailDto>> GetMappedMenu(string moduleId, int roleId);
        Task<List<Module>> GetModuleList();
        Task<List<PermissionsDataDto>> GetallpermissionList(int ModuleId, int RoleId);
        Task<List<PermissionDto>> GetMappedMenuWithAction(int moduleId, int roleId);
        Task<bool> AddUpdatePermission(List<MenuActionRoleMapDto> model);
        Task<bool> ValidatePermission(ViewAction action, int roleId, string moduleGuid, string url);
    }
}
