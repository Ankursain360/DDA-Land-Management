using Dto.GIS;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
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
        Task<List<Village>> GetVillageDetails(int villageId);
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
        Task<List<Gisdashed>> GetDashedDetails(int villageId);
        Task<List<Gisclose>> GetCloseDetails(int villageId);
        Task<List<Gisclosetext>> GetCloseTextDetails(int villageId);
        Task<List<Gisdimtext>> GetDimTextDetails(int villageId);
        Task<List<Gisfieldboun>> GetFieldBounDetails(int villageId);
        Task<List<Giskilla>> GetKillaDetails(int villageId);
        Task<List<Giskhasrano>> GetKhasraNoDetails(int villageId);
        Task<List<Giskhasraline>> GetKhasraLineDetails(int villageId);
        Task<List<Giskhasraboundary>> GetKhasraBoundaryDetails(int villageId);
        Task<List<Giskachapakaline>> GetKachaPakaLineDetails(int villageId);
        Task<List<Gisinner>> GetInnerDetails(int villageId);
        Task<List<Gisnali>> GetNaliDetails(int villageId);
        Task<List<Gisrailwayline>> GetRailwayLineDetails(int villageId);
        Task<List<Gissaheda>> GetSahedaDetails(int villageId);
        Task<List<Gisroad>> GetRoadDetails(int villageId);
        Task<List<Giszero>> GetZeroDetails(int villageId);
        Task<List<Gisvillagetext>> GetVillageTextDetails(int villageId);
        Task<List<Gisvillageboundary>> GetVillageBoundaryDetails(int villageId);
        Task<List<VillageDto>> GetVillageAutoCompleteDetails(string prefix);
        Task<List<gisDataTemp>> GetInfrastructureDetails(int villageId);
        Task<List<Gisdata>> GetGisDataLayersDetails(int villageId);
        Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetails(int villageId, string khasraNo, string RectNo);
        Task<List<GISKhasraBasisOtherDetailsDto>> GetKhasraBasisOtherDetailsForCourtCases(int villageId, string khasraNo,string RectNo);
        Task<List<GISKhasraDto>> GetKhasraList(int villageId);
        Task<List<Gisdata>> GetKhasraNoPolygon(int gisDataId);
        Task<GISKhasraUpdateResponseDto> UpdatekhasraNo(int khasraid, string khasraNo,int Userid);

        Task<List<Gisdata>> GetGCPList(int villageId);

        Task<List<GISKhasraExport>> GetKhasraListforExport(int villageId);

        Task<PagedResult<AIchangedetectiondata>> GetChangeDetectionData(AIchangeDetectionSearchDto model);
        Task<AIchangedetectiondata> GetAIchangedetectionImageDetails(int id);
        Task<bool> InsertchangeDetectiondata(ChangeDetectionDto dto); 
    }
}
