
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class WatchAndWardApprovalService : EntityService<Watchandward>, IWatchAndWardApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWatchAndWardApprovalRepository _watchAndWardApprovalRepository;
        public WatchAndWardApprovalService(IUnitOfWork unitOfWork, IWatchAndWardApprovalRepository watchAndWardApprovalRepository)
        : base(unitOfWork, watchAndWardApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _watchAndWardApprovalRepository = watchAndWardApprovalRepository;
        }

        public async Task<Watchandward> FetchSingleResult(int id)
        {
            var result = await _watchAndWardApprovalRepository.FetchSingleResult(id);
            return result;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _watchAndWardApprovalRepository.GetAllKhasra();
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            return await _watchAndWardApprovalRepository.GetAllLocality();
        }
        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _watchAndWardApprovalRepository.GetAllWatchandward();
        }

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardApprovalSearchDto model, int userId,int zoneId, int deprtId )
        {
            return await _watchAndWardApprovalRepository.GetPagedWatchandward(model,  userId, zoneId, deprtId);
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _watchAndWardApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
    }
}
