using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class GISService : EntityService<Zone>, IGISService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGISSRepository _iGISSRepository;
        public GISService(IUnitOfWork unitOfWork, IGISSRepository iGisSRepository) : base(unitOfWork, iGisSRepository)
        {
            _unitOfWork = unitOfWork;
            _iGISSRepository = iGisSRepository;
        }

        public async Task<List<Gisaabadi>> GetAbadiDetails(int villageId)
        {
            return await _iGISSRepository.GetAbadiDetails(villageId);
        }

        public async Task<List<Gisburji>> GetBurjiDetails(int villageId)
        {
            return await _iGISSRepository.GetBurjiDetails(villageId);
        }

        public async Task<List<GISclean>> GetCleanDetails(int villageId)
        {
            return await _iGISSRepository.GetCleanDetails(villageId);
        }

        public async Task<List<Giscleantext>> GetCleantextDetails(int villageId)
        {
            return await _iGISSRepository.GetCleantextDetails(villageId);
        }

        public async Task<Gisclose> GetCloseDetails(int villageId)
        {
            return await _iGISSRepository.GetCloseDetails(villageId);
        }

        public async Task<Gisclosetext> GetCloseTextDetails(int villageId)
        {
            return await _iGISSRepository.GetCloseTextDetails(villageId);
        }

        public async Task<Gisdashed> GetDashedDetails(int villageId)
        {
            return await _iGISSRepository.GetDashedDetails(villageId);
        }

        public async Task<List<Gisdim>> GetDimDetails(int villageId)
        {
            return await _iGISSRepository.GetDimDetails(villageId);
        }

        public async Task<Gisdimtext> GetDimTextDetails(int villageId)
        {
            return await _iGISSRepository.GetDimTextDetails(villageId);
        }

        public async Task<List<GISencroachment>> GetEncroachmentDetails(int villageId)
        {
            return await _iGISSRepository.GetEncroachmentDetails(villageId);
        }

        public async Task<Gisfieldboun> GetFieldBounDetails(int villageId)
        {
            return await _iGISSRepository.GetFieldBounDetails(villageId);
        }

        public async Task<List<Gisgosha>> GetGoshaDetails(int villageId)
        {
            return await _iGISSRepository.GetGoshaDetails(villageId);
        }

        public async Task<List<Gisgrid>> GetGridDetails(int villageId)
        {
            return await _iGISSRepository.GetGridDetails(villageId);
        }

        public async Task<List<State>> GetInitiallyStateDetails()
        {
            return await _iGISSRepository.GetInitiallyStateDetails();
        }

        public async Task<Gisinner> GetInnerDetails(int villageId)
        {
            return await _iGISSRepository.GetInnerDetails(villageId);
        }

        public async Task<Giskachapakaline> GetKachaPakaLineDetails(int villageId)
        {
            return await _iGISSRepository.GetKachaPakaLineDetails(villageId);
        }

        public async Task<Giskhasraboundary> GetKhasraBoundaryDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraBoundaryDetails(villageId);
        }

        public async Task<Giskhasraline> GetKhasraLineDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraLineDetails(villageId);
        }

        public async Task<Giskhasrano> GetKhasraNoDetails(int villageId)
        {
            return await _iGISSRepository.GetKhasraNoDetails(villageId);
        }

        public async Task<Giskilla> GetKillaDetails(int villageId)
        {
            return await _iGISSRepository.GetKillaDetails(villageId);
        }

        public async Task<List<GISnala>> GetNalaDetails(int villageId)
        {
            return await _iGISSRepository.GetNalaDetails(villageId);
        }

        public async Task<Gisnali> GetNaliDetails(int villageId)
        {
            return await _iGISSRepository.GetNaliDetails(villageId);
        }

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _iGISSRepository.GetPlotList(VillageId);
        }

        public async Task<Gisrailwayline> GetRailwayLineDetails(int villageId)
        {
            return await _iGISSRepository.GetRailwayLineDetails(villageId);
        }

        public async Task<Gisroad> GetRoadDetails(int villageId)
        {
            return await _iGISSRepository.GetRoadDetails(villageId);
        }

        public async Task<Gissaheda> GetSahedaDetails(int villageId)
        {
            return await _iGISSRepository.GetSahedaDetails(villageId);
        }

        public async Task<List<Gistext>> GetTextDetails(int villageId)
        {
            return await _iGISSRepository.GetTextDetails(villageId);
        }

        public async  Task<List<Gistrijunction>> GetTriJunctionDetails(int villageId)
        {
            return await _iGISSRepository.GetTriJunctionDetails(villageId);
        }

        public async Task<Gisvillageboundary> GetVillageBoundaryDetails(int villageId)
        {
            return await _iGISSRepository.GetVillageBoundaryDetails(villageId);
        }

        public async Task<List<Village>> GetVillageDetails(int villageId, int zoneId)
        {
            return await _iGISSRepository.GetVillageDetails(villageId, zoneId);
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _iGISSRepository.GetVillageList(ZoneId);
        }

        public async Task<Gisvillagetext> GetVillageTextDetails(int villageId)
        {
            return await _iGISSRepository.GetVillageTextDetails(villageId);
        }

        public async Task<Giszero> GetZeroDetails(int villageId)
        {
            return await _iGISSRepository.GetZeroDetails(villageId);
        }

        public async Task<List<Zone>> GetZoneDetails(int zoneId)
        {
            return await _iGISSRepository.GetZoneDetails(zoneId);
        }

        public async Task<List<Zone>> GetZoneList()
        {
            return await _iGISSRepository.GetZoneList();
        }
    }
}
