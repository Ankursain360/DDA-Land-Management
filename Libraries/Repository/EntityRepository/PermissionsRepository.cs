using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Menuactionrolemap>> GetPermission(int moduleId, int roleId) {
            try
            {
                var x = await _dbContext.Menuactionrolemap
                    .Include(a => a.Menu)
                    .Where(a => a.Menu.ModuleId == moduleId
                            && a.RoleId == roleId)
                    .ToListAsync();
                return x;
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }
    }
}