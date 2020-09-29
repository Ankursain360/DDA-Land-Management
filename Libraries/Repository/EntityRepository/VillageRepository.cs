using Dto.Search;
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
    public class VillageRepository : GenericRepository<Village>, IVillageRepository
    {
        public VillageRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Village>> GetPagedVillage(VillageSearchDto model)
        {
            return await _dbContext.Village.GetPaged<Village>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Village>> GetVillage()
        {
            return await _dbContext.Village.Include(x => x.Zone).OrderByDescending(x=>x.Id).ToListAsync();
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
