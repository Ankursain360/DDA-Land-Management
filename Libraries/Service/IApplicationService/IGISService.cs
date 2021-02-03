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

    }
}
