using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Role>> GetRole()
        {
            return await _dbContext.Role.Include(x => x.Zone).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.ToListAsync();
            return zoneList;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Village.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
