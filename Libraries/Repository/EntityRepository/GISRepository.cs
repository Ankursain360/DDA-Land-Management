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
                                    .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                    .ToListAsync();
        }

        public async Task<List<Gisburji>> GetBurjiDetails(int villageId)
        {
            return await _dbContext.Gisburji
                                   .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                   .ToListAsync();
        }

        public async Task<List<Gisclean>> GetCleanDetails(int villageId)
        {
            try
            {
                return await _dbContext.Gisclean
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Giscleantext>> GetCleantextDetails(int villageId)
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

        public async Task<Gisclose> GetCloseDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gisclosetext> GetCloseTextDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gisdashed> GetDashedDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _dbContext.Gisdim
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<Gisdimtext> GetDimTextDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gisencroachment>> GetEncroachmentDetails(int villageId)
        {
            try
            {
                return await _dbContext.Gisencroachment
                                       .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Gisfieldboun> GetFieldBounDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gisgosha>> GetGoshaDetails(int villageId)
        {
            return await _dbContext.Gisgosha
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
        }

        public async Task<List<Gisgrid>> GetGridDetails(int villageId)
        {
            return await _dbContext.Gisgrid
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<State>> GetInitiallyStateDetails()
        {
            return await _dbContext.State
                                 .Where(x =>  x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<Gisinner> GetInnerDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giskachapakaline> GetKachaPakaLineDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giskhasraboundary> GetKhasraBoundaryDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giskhasraline> GetKhasraLineDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giskhasrano> GetKhasraNoDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giskilla> GetKillaDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gisnala>> GetNalaDetails(int villageId)
        {
            return await _dbContext.Gisnala
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<Gisnali> GetNaliDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _dbContext.Plot.Where(x => x.VillageId == VillageId && x.IsActive == 1).ToListAsync();
        }

        public async Task<Gisrailwayline> GetRailwayLineDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gisroad> GetRoadDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Gissaheda> GetSahedaDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gistext>> GetTextDetails(int villageId)
        {
            
            var data = await _dbContext.Gistext
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
            return data;
        }

        public async Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId)
        {
            return await _dbContext.Gistrijunction
                                .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                .ToListAsync();
        }

        public Task<Gisvillageboundary> GetVillageBoundaryDetails(int villageId)
        {
            throw new NotImplementedException();
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

        public async Task<Gisvillagetext> GetVillageTextDetails(int villageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Giszero> GetZeroDetails(int villageId)
        {
            throw new NotImplementedException();
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
