using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Gisaabadi>> GetAbadiDetails(int villageId)
        {
            return await _dbContext.Gisaabadi
                                    .Include(x => x.Village)
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                    .ToListAsync();
        }

        public async Task<List<Gisburji>> GetBurjiDetails(int villageId)
        {
            return await _dbContext.Gisburji
                                   .Include(x => x.Village)
                                   .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                   .ToListAsync();
        }

        public async Task<List<GISClean>> GetCleanDetails(int villageId)
        {
            try
            {

                return await _dbContext.Gisclean
                                       .Include(x => x.Village)
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GisCleanText>> GetCleantextDetails(int villageId)
        {
            try
            {

                return await _dbContext.Giscleantext
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _dbContext.Gisdim
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _dbContext.Plot.Where(x => x.VillageId == VillageId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Village>> GetVillageDetails(int villageId, int zoneId)
        {
            return await _dbContext.Village
                                    .Where(x => x.ZoneId == zoneId && x.IsActive == 1 && x.Id == villageId)
                                    .ToListAsync();
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _dbContext.Village.Where(x => x.ZoneId == ZoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Zone>> GetZoneDetails(int zoneId)
        {
            return await _dbContext.Zone.Include(x => x.Village).Where(x => x.IsActive == 1 && x.Id == zoneId).ToListAsync();
        }

        public async Task<List<Zone>> GetZoneList()
        {
            return await _dbContext.Zone.Include(x => x.Village).Where(x => x.IsActive == 1 && x.Xcoordinate !=null).ToListAsync();
        }
    }
}
