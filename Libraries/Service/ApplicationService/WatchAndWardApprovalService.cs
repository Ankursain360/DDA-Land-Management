
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
            var result = await _watchAndWardApprovalRepository.FindBy(a => a.Id == id);
            Watchandward model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _watchAndWardApprovalRepository.GetAllKhasra();
        }

        public async Task<List<Village>> GetAllVillage()
        {
            return await _watchAndWardApprovalRepository.GetAllVillage();
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            return await _watchAndWardApprovalRepository.GetAllLocality();
        }
        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _watchAndWardApprovalRepository.GetAllWatchandward();
        }

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model, int userId)
        {
            return await _watchAndWardApprovalRepository.GetPagedWatchandward(model,  userId);
        }

        public Task<bool> Update(int id, Watchandward watchandward)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Watchandward watchandward)
        {
            throw new NotImplementedException();
        }
    }
}
