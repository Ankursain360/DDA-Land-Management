using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IMenuService : IEntityService<Menu>
    {
        Task<List<Menu>> GetAllMenu(); 
        
        Task<List<Module>> GetAllModule(); 
        Task<List<Menu>> GetMenuUsingRepo();
        Task<bool> Update(int id, Menu menu); 
        Task<bool> Create(Menu menu);
        Task<Menu> FetchSingleResult(int id); 
        Task<bool> Delete(int id);   
        Task<bool> CheckUniqueName(int Id, string Name, int ModuleId);   
        //Task<bool> CheckUniqueCode(int id, string code);

        Task<PagedResult<Menu>> GetPagedMenu(MenuSearchDto model);
        
    }
}
