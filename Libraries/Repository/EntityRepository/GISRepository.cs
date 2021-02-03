using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class GISRepository : GenericRepository<Zone>, IGISSRepository
    {
        public GISRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _dbContext.Plot.Where(x => x.VillageId == VillageId).ToListAsync();
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _dbContext.Village.Where(x => x.ZoneId == ZoneId).ToListAsync();
        }

        public async Task<List<Zone>> GetZoneList()
        {
            return await _dbContext.Zone.ToListAsync();
        }
    }
}
