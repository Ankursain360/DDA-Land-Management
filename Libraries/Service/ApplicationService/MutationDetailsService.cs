using Dto.Search;
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
    public class MutationDetailsService : EntityService<Mutationdetails>, IMutationDetailsService

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

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _mutationDetailsRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _mutationDetailsRepository.GetDistrictList();
            return districtList;
        }

        public Task<bool> Update(int id, Mutationdetails details)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Mutationdetails> GetPhotoPropFile(int id)
        {
            return await _mutationDetailsRepository.GetPhotoPropFile(id);

        }

        public async Task<Mutationdetails> SaveMutationAtsFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationAtsFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationGPAFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationGPAFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationMoneyReceiptFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationSignSPCFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationSignSPCFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationAddressProofFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationAddressProofFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationAffitDevitFilePath(id);
        }
        public async Task<Mutationdetails> SaveMutationIndemnityFilePath(int id)
        {
            return await _mutationDetailsRepository.SaveMutationIndemnityFilePath(id);
        }

        public async Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage)
        {
            oldDamage.CreatedBy = 1;
            oldDamage.CreatedDate = DateTime.Now;
            oldDamage.IsActive = 1;
            return await _mutationDetailsRepository.SaveMutationOldDamage(oldDamage);
        }
        public async Task<Damagepayeeregistertemp> FetchMutationDetailsUserId(int userId)
        {
            return await _mutationDetailsRepository.FetchMutationDetailsUserId(userId);
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedSubsitutionMutationDetails(SubstitutionMutationDetailsDto model)
        {
            return await _mutationDetailsRepository.GetPagedSubsitutionMutationDetails(model);
        }
    }
}
