using Dto.Master;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
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

        public async Task<List<Gisclose>> GetCloseDetails(int villageId)
        {
            return await _dbContext.Gisclose
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisclosetext>> GetCloseTextDetails(int villageId)
        {
            return await _dbContext.Gisclosetext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisdashed>> GetDashedDetails(int villageId)
        {
            return await _dbContext.Gisdashed
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _dbContext.Gisdim
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisdimtext>> GetDimTextDetails(int villageId)
        {
            return await _dbContext.Gisdimtext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
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

        public async Task<List<Gisfieldboun>> GetFieldBounDetails(int villageId)
        {
            return await _dbContext.Gisfieldboun
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
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

        public async Task<List<Gislayer>> GetInfrastructureDetails(int villageId)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("GISInfrastructureColorCodeBind")
                                            .WithSqlParams( ("p_villageid", villageId)
                                            )
                                            .ExecuteStoredProcedureAsync<Gislayer>();

                return (List<Gislayer>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<State>> GetInitiallyStateDetails()
        {
            return await _dbContext.State
                                 .Where(x =>  x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisinner>> GetInnerDetails(int villageId)
        {
            return await _dbContext.Gisinner
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskachapakaline>> GetKachaPakaLineDetails(int villageId)
        {
            return await _dbContext.Giskachapakaline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskhasraboundary>> GetKhasraBoundaryDetails(int villageId)
        {
            return await _dbContext.Giskhasraboundary
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskhasraline>> GetKhasraLineDetails(int villageId)
        {
            return await _dbContext.Giskhasraline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskhasrano>> GetKhasraNoDetails(int villageId)
        {
            return await _dbContext.Giskhasrano
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Giskilla>> GetKillaDetails(int villageId)
        {
            return await _dbContext.Giskilla
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisnala>> GetNalaDetails(int villageId)
        {
            return await _dbContext.Gisnala
                                 .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                 .ToListAsync();
        }

        public async Task<List<Gisnali>> GetNaliDetails(int villageId)
        {
            return await _dbContext.Gisnali
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _dbContext.Plot.Where(x => x.VillageId == VillageId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Gisrailwayline>> GetRailwayLineDetails(int villageId)
        {
            return await _dbContext.Gisrailwayline
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gisroad>> GetRoadDetails(int villageId)
        {
            return await _dbContext.Gisroad
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Gissaheda>> GetSahedaDetails(int villageId)
        {
            return await _dbContext.Gissaheda
                                     .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                     .ToListAsync();
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

        public async Task<List<Village>> GetVillageAutoCompleteDetails(string prefix)
        {
            var data = await _dbContext.Village.Where(x => x.Name.Contains(prefix)).ToListAsync();
            return data;

        }

        public async Task<List<Gisvillageboundary>> GetVillageBoundaryDetails(int villageId)
        {
            return await _dbContext.Gisvillageboundary
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Village>> GetVillageDetails(int villageId)
        {
            return await _dbContext.Village
                                    .Where(x =>  x.IsActive == 1 && x.Id == villageId)
                                    .ToListAsync();
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _dbContext.Village.Where(x => x.ZoneId == ZoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Gisvillagetext>> GetVillageTextDetails(int villageId)
        {
            return await _dbContext.Gisvillagetext
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
        }

        public async Task<List<Giszero>> GetZeroDetails(int villageId)
        {
            return await _dbContext.Giszero
                                      .Where(x => x.VillageId == villageId && x.IsActive == 1)
                                      .ToListAsync();
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
