using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class PermissionsRepository : GenericRepository<Menuactionrolemap>, IPermissionsRepository
    {
        public PermissionsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Module>> GetModuleList()
        {
            return await _dbContext.Module
                .Where(x => x.IsActive == 1)
                .ToListAsync();
        }

        public async Task<List<Menuactionrolemap>> GetPermission(string moduleId, int roleId)
        {
            var result = await _dbContext.Menuactionrolemap
                .Include(a => a.Menu.Module)
                .Include(a => a.Menu)
                .Where(a => a.Menu.Module.Guid == moduleId
                        && a.RoleId == roleId && a.Menu.IsActive == 1)
                .ToListAsync();
            return result;
        }

        public async Task<List<Menu>> GetMappedMenuWithAction(int moduleId)
        {
            try
            {
                var result = await _dbContext.Menu
                .Include(a => a.Menuactionrolemap)
                .ThenInclude(a => a.Action)
                .Where(a => a.ModuleId == moduleId && a.IsActive == 1)
                .ToListAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }
        public async Task<List<Menuactionrolemap>> MenuactionrolemapList(int ModuleId, int RoleId)
        {
            var data = await _dbContext.Menuactionrolemap
                                                         .Include(x => x.Module)
                                                         .Include(x => x.Role)
                                                         .Include(x => x.Action)
                                                         .Include(x => x.Menu)
                                                         .Where(x => x.ModuleId == ModuleId && x.RoleId == RoleId && x.Menu.IsActive == 1).ToListAsync();
            return data;
        }
        public async Task<bool> AuthorizeUser(string actionName, int roleId, int moduleId, int menuId)
        {
            return await _dbContext.Menuactionrolemap
                .Include(a => a.Action)
                .Include(a => a.Menu)
                .Where(a => a.Action.Name == actionName
                    && a.RoleId == roleId
                    && a.ModuleId == moduleId
                    && a.MenuId == menuId
                    )
                .AnyAsync();
        }

        
    }
}