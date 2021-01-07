using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        Task<List<Menu>> GetAllMenu();
        Task<List<Menu>> GetAllParentmenu();
        Task<List<Module>> GetAllModule();
        Task<bool> AnyName(int Id,int ModuleId, string Name);
        //Task<bool> AnyCode(int id, string name);
        Task<PagedResult<Menu>> GetPagedMenu(MenuSearchDto model);
        int GetMenuIdByUrl(string url, int id);
    }
}
