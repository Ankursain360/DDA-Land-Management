using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
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

        public async Task<object> GetPlotList(int? VillageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Village> GetVillageList(int? ZoneId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Zone> GetZoneList()
        {
            throw new System.NotImplementedException();
        }
    }
}
