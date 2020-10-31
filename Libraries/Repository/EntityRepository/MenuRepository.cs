using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Libraries.Repository.EntityRepository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Menu>> GetPagedMenu(MenuSearchDto model)
        {
            return await _dbContext.Menu
                        .Include(x => x.Module)
                       
                            .Where(x => x.IsActive == 1)
                            .OrderBy(s => s.Module.Name)
                           
                            .ThenBy(s => s.Name)
                        .GetPaged<Menu>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Module>> GetAllModule()
        {
            List<Module> modulelist = await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
            return modulelist;
        }

        public async Task<bool> AnyName(int Id, string Name, int ModuleId)
        {
            return await _dbContext.Menu.AnyAsync(t => t.Id != Id && t.ModuleId == ModuleId && t.IsActive == 1 && t.Name.ToLower() == Name.ToLower());
        }


        public async Task<List<Menu>> GetAllMenu()
        {
            var data = await _dbContext.Menu.Include(x => x.Module).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }







    }
}
