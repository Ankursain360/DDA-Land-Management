using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IGISSRepository : IGenericRepository<Zone>
    {
        Task<List<Zone>> GetZoneList();
        Task<List<Village>> GetVillageList(int ZoneId);
        Task<List<Plot>> GetPlotList(int VillageId);
        Task<List<Zone>> GetZoneDetails(int zoneId);
        Task<List<Village>> GetVillageDetails(int villageId, int zoneId);
        Task<List<Gisaabadi>> GetAbadiDetails(int villageId);
    }
}
