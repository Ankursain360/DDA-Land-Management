using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using System.Linq;

namespace Libraries.Repository.EntityRepository
{  
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Page>> GetPagedPage(PageSearchDto model)
        {
            return await _dbContext.Page
                .Include(x => x.Menu)
                .Where(x => x.IsActive==1)
                .GetPaged<Page>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Page>> GetPage()
        {
            return await _dbContext.Page.ToListAsync();
        }
        public async Task<List<Page>> GetAllPage()
        {
            return await _dbContext.Page.Include(x=>x.Menu).ToListAsync();
        }
        public async Task<List<Menu>> GetAllMenu()
        {
            List<Menu> menuList = await _dbContext.Menu.Where(x => x.IsActive == 1).ToListAsync();
            return menuList;
        }
        public async Task<bool> Any(int id, string name,int MenuId)
        {
            return await _dbContext.Page.AnyAsync(t => t.Id != id && t.MenuId == MenuId && t.Name.ToLower() == name.ToLower());
        }

       
    }
}
