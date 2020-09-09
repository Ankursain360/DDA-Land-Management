using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class VillageRepository : GenericRepository<Village>, IVillageRepository
    {
        public VillageRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Village>> GetVillage()
        {
            return await _dbContext.Village.ToListAsync();
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.ToListAsync();
            return zoneList;
        }

    }
}
