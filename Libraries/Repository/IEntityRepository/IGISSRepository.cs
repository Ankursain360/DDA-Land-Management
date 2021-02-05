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
    }
}
