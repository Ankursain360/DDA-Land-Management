using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
   
     public interface IPageRepository : IGenericRepository<Page>
    {
        Task<List<Page>> GetPage();
        Task<List<Menu>> GetAllMenu();
        Task<bool> Any(int id, string name);
        Task<List<Page>> GetAllPage();
        Task<PagedResult<Page>> GetPagedPage(PageSearchDto model);
    }
}
