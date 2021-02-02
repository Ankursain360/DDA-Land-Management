using Libraries.Model.Entity;
using Libraries.Service.Common;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IGISService : IEntityService<Zone>
    {
        Task<Village> GetVillageList(int? ZoneId);
        Task<object> GetPlotList(int? VillageId);
        Task<Zone> GetZoneList();
    }
}
