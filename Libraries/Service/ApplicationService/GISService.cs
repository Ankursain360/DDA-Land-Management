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

        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _iGISSRepository.GetPlotList(VillageId);
        }

        public async Task<List<Village>> GetVillageDetails(int villageId, int zoneId)
        {
            return await _iGISSRepository.GetVillageDetails(villageId, zoneId);
        }

        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _iGISSRepository.GetVillageList(ZoneId);
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
