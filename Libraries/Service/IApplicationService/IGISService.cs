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
        Task<List<Gisclean>> GetCleanDetails(int villageId);
        Task<List<Giscleantext>> GetCleantextDetails(int villageId);
        Task<List<Gisdim>> GetDimDetails(int villageId);
        Task<List<Gisencroachment>> GetEncroachmentDetails(int villageId);
        Task<List<Gisgosha>> GetGoshaDetails(int villageId);
        Task<List<Gisgrid>> GetGridDetails(int villageId);
        Task<List<Gisnala>> GetNalaDetails(int villageId);
        Task<List<Gistext>> GetTextDetails(int villageId);
        Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId);
        Task<List<State>> GetInitiallyStateDetails();
        Task<Gisdashed> GetDashedDetails(int villageId);
        Task<Gisclose> GetCloseDetails(int villageId);
        Task<Gisclosetext> GetCloseTextDetails(int villageId);
        Task<Gisdimtext> GetDimTextDetails(int villageId);
        Task<Gisfieldboun> GetFieldBounDetails(int villageId);
        Task<Giskilla> GetKillaDetails(int villageId);
        Task<Giskhasrano> GetKhasraNoDetails(int villageId);
        Task<Giskhasraline> GetKhasraLineDetails(int villageId);
        Task<Giskhasraboundary> GetKhasraBoundaryDetails(int villageId);
        Task<Giskachapakaline> GetKachaPakaLineDetails(int villageId);
        Task<Gisinner> GetInnerDetails(int villageId);
        Task<Gisnali> GetNaliDetails(int villageId);
        Task<Gisrailwayline> GetRailwayLineDetails(int villageId);
        Task<Gissaheda> GetSahedaDetails(int villageId);
        Task<Gisroad> GetRoadDetails(int villageId);
        Task<Giszero> GetZeroDetails(int villageId);
        Task<Gisvillagetext> GetVillageTextDetails(int villageId);
        Task<Gisvillageboundary> GetVillageBoundaryDetails(int villageId);
    }
}
