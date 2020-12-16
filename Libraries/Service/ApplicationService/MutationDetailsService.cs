using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class MutationDetailsService: EntityService<Mutationdetails>, IMutationDetailsService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMutationDetailsRepository _mutationDetailsRepository;
        protected readonly DataContext _dbContext;

        public MutationDetailsService(IUnitOfWork unitOfWork, IMutationDetailsRepository mutationDetailsRepository, DataContext dbContext)
       : base(unitOfWork, mutationDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mutationDetailsRepository = mutationDetailsRepository;
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Mutationdetails details)
        {
            details.CreatedBy = 1;
            details.CreatedDate = DateTime.Now;
            _mutationDetailsRepository.Add(details);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Mutationdetails>> GetAllMutationDetails()
        {
            return await _mutationDetailsRepository.GetAllMutationDetails();
        }

        public async Task<List<Locality>> GetAllLocality(int zoneId)
        {
            List<Locality> localityList = await _mutationDetailsRepository.GetAllLocality(zoneId);
            return localityList;
        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _mutationDetailsRepository.GetAllZone();
            return zoneList;
        }

        public Task<bool> Update(int id, Mutationdetails details)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
