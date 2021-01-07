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
            var data = await _dbContext.Menu
                        .Include(x => x.Module)
                        .Include(x => x.ParentMenu)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.parentname) || x.ParentMenu.Name.Contains(model.parentname))
                            && (string.IsNullOrEmpty(model.modulename) || x.Module.Name.Contains(model.modulename)) )
                            .OrderBy(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))                               
                            .OrderByDescending(s => s.IsActive)
                            .ThenBy(s => s.Name)
                            .ThenBy(s => s.Module.Name)
                            
                        .GetPaged<Menu>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("MODULENAME"):
                        data.Results = data.Results.OrderBy(x => x.Module.Name).ToList();
                        break;
                    case ("PARENTNAME"):
                        data.Results = data.Results.OrderBy(x => (x.ParentMenu!=null? x.ParentMenu.Name:null)).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("MODULENAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Module.Name).ToList();
                        break;
                    case ("PARENTNAME"):
                        data.Results = data.Results.OrderByDescending(x => (x.ParentMenu != null ? x.ParentMenu.Name : null)).ToList();
                        break;

                }
            }
          
            return data;
        }






        public async Task<List<Module>> GetAllModule()
        {
            List<Module> modulelist = await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
            return modulelist;
        }
        public async Task<List<Menu>> GetAllParentmenu()
        {
            List<Menu> parentmenulist = await _dbContext.Menu.Where(x => x.IsActive == 1).ToListAsync();
            return parentmenulist;
        }
        public async Task<bool> AnyName(int Id, int ModuleId, string Name)
        {
            return await _dbContext.Menu.AnyAsync(t => t.Id != Id && t.ModuleId == ModuleId && t.Name.ToLower() == Name.ToLower() && t.IsActive == 1);

        }






        //public async Task<bool> AnyCode(int id, string code)
        //{
        //    return await _dbContext.Locality.AnyAsync(t => t.Id != id && t.LocalityCode.ToLower() == code.ToLower());
        //}

        public async Task<List<Menu>> GetAllMenu()
        {
            var data = await _dbContext.Menu.Include(x => x.Module).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }

        public  int GetMenuIdByUrl(string url, int id)
        {
           int menuId =   (from x in _dbContext.Menu
                 where x.ModuleId == id && x.Url.Contains(url) /*x.Url == url*/
                 select x.Id).First();
            return menuId;
        }
    }
}
