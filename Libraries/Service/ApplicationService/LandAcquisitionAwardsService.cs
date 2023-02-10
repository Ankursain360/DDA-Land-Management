using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class LandAcquisitionAwardsService : EntityService<LandAcquisitionAwards>, ILandAcquisitionAwardsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandAcquisitionAwardsRepository _landAcquisitionAwardsRepository;
        private readonly DataContext _dbContext;

        public LandAcquisitionAwardsService(IUnitOfWork unitOfWork, ILandAcquisitionAwardsRepository landAcquisitionAwardsRepository  , DataContext dbContext): base(unitOfWork, landAcquisitionAwardsRepository)
        {
            _unitOfWork = unitOfWork;
            _landAcquisitionAwardsRepository = landAcquisitionAwardsRepository;
            _dbContext = dbContext; 
        }
        //public async Task<List<Locality>> GetLocalityList()
        //{
        //    return await _landAcquisitionAwardsRepository.GetLocalityList();
        //}

        public async Task<PagedResult<LandAcquisitionAwards>> GetPagedLandAcquisitionAwards(LandAcquisitionAwardsDto model)
        {
            return await _landAcquisitionAwardsRepository.GetPagedLandAcquisitionAwards(model);
        }
        public async Task<LandAcquisitionAwards> FetchDocumentDetails(int id)
        {
            return await _landAcquisitionAwardsRepository.FetchDocumentDetails(id);
        }
        public async Task<List<LandAcquisitionAwards>> GetLandAcquisitionAwardsList(LandAcquisitionAwardsSearchDto model)
        {
            return await _landAcquisitionAwardsRepository.GetLandAcquisitionAwardsList(model);
        }
        public async Task<List<LandAcquisitionAwards>> GetAllAcquisitionAwards()
        {
            return await _landAcquisitionAwardsRepository.GetAllAcquisitionAwards();
        }
    }
}
