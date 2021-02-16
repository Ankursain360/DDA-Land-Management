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

namespace Service.ApplicationService
{
    public class MutationService : EntityService<Mutation>, IMutationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMutationRepository _mutationRepository;

        public MutationService(IUnitOfWork unitOfWork, IMutationRepository mutationRepository)
        : base(unitOfWork, mutationRepository)
        {
            _unitOfWork = unitOfWork;
            _mutationRepository = mutationRepository;
        }

        public async Task<PagedResult<Mutation>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model)
        {
            return await _mutationRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Mutation mutation)
        {
            try
            {
                mutation.IsActive = 1;
                mutation.CreatedDate = DateTime.Now;
                _mutationRepository.Add(mutation);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Mutation> FetchSingleResult(int id)
        {
            return await _mutationRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Mutation mutation)
        {
            var result = await _mutationRepository.FindBy(a => a.Id == id);
            Mutation model = result.FirstOrDefault();
            model.AcquiredVillageId = mutation.AcquiredVillageId;
            model.KhasraId = mutation.KhasraId;
            model.MutationOwnerLessee = mutation.MutationOwnerLessee;
            model.MutationNo = mutation.MutationNo;
            model.MutationFees = mutation.MutationFees;
            model.MutationDate = mutation.MutationDate;
            model.NewAccountCode = mutation.NewAccountCode;
            model.JaraiSakniCode = mutation.JaraiSakniCode;
            model.MutationType = mutation.MutationType;
            model.Remark = mutation.Remark;
            model.IsActive = mutation.IsActive;
            model.ModifiedDate = DateTime.Now;
            _mutationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _mutationRepository.FindBy(a => a.Id == id);
            Mutation model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _mutationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> CheckUniqueName(int id, string fileNo)
        {
            return await _mutationRepository.Any(id, fileNo);
        }
        public async Task<List<Acquiredlandvillage>> GetVillageList()
        {
            return await _mutationRepository.GetVillageList();
        }

        public async Task<List<Khasra>> GetKhasraList(int id)
        {
            return await _mutationRepository.GetKhasraList(id);
        }

        public async Task<List<Mutationparticulars>> GetMutationParticulars(int id)
        {
            return await _mutationRepository.GetMutationParticulars(id);
        }

        public async Task<bool> SaveMutationParticulars(List<Mutationparticulars> mutationparticulars)
        {
            mutationparticulars.ForEach(x => x.CreatedDate = DateTime.Now);
            return await _mutationRepository.SaveMutationParticulars(mutationparticulars);
        }

        public async Task<bool> DeleteMutationParticulars(int id)
        {
            return await _mutationRepository.DeleteMutationParticulars(id);
        }
    }
}
