using Libraries.Model.Entity;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IGISService : IEntityService<Zone>
    {
        Task<List<Zone>> GetZoneList();
        Task<List<Village>> GetVillageList(int ZoneId);
        Task<List<Plot>> GetPlotList(int VillageId);
        Task<List<Zone>> GetZoneDetails(int zoneId);
        Task<List<Village>> GetVillageDetails(int villageId, int zoneId);
        Task<List<Gisaabadi>> GetAbadiDetails(int villageId);
        Task<List<Gisburji>> GetBurjiDetails(int villageId);
        Task<List<GISClean>> GetCleanDetails(int villageId);
        Task<List<GisCleanText>> GetCleantextDetails(int villageId);
        Task<List<Gisdim>> GetDimDetails(int villageId);
        Task<List<GISEncroachment>> GetEncroachmentDetails(int villageId);
        Task<List<Gisgosha>> GetGoshaDetails(int villageId);
        Task<List<Gisgrid>> GetGridDetails(int villageId);
        Task<List<GISnala>> GetNalaDetails(int villageId);
        Task<List<Gistext>> GetTextDetails(int villageId);
        Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId);
    }
}
