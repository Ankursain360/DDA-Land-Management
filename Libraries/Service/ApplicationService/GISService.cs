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
        public async Task<List<Plot>> GetPlotList(int VillageId)
        {
            return await _iGISSRepository.GetPlotList(VillageId);
        }
        public async Task<List<Village>> GetVillageList(int ZoneId)
        {
            return await _iGISSRepository.GetVillageList(ZoneId);
        }
        public async Task<List<Zone>> GetZoneList()
        {
            return await _iGISSRepository.GetZoneList();
        }
    }
}
