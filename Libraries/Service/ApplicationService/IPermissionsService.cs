using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Component;
using Libraries.Model.Entity;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IPermissionsService : IEntityService<Menuactionrolemap>
    {
        Task<List<MenuDetailDto>> GetMappedMenu(int moduleId, int roleId);
        Task<List<Module>> GetModuleList();
    }
}
