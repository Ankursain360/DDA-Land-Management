using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class LeasesubpurposeService : EntityService<Leasesubpurpose>, ILeasesubpurposeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasesubpurposeRepository _LeasesubpurposeRepository;
        private readonly IMapper _mapper;
        public LeasesubpurposeService(IUnitOfWork unitOfWork,
            ILeasesubpurposeRepository LeasesubpurposeRepository,
            IMapper mapper)
        : base(unitOfWork, LeasesubpurposeRepository)
        {
            _unitOfWork = unitOfWork;
            _LeasesubpurposeRepository = LeasesubpurposeRepository;
            _mapper = mapper;
        }
        public async Task<Leasesubpurpose> FetchSingleResult(int id)
        {
            return await _LeasesubpurposeRepository.FetchSingleResult(id);
        }
        public async Task<bool> Update(int id, Leasesubpurpose leasesubpurpose)
        {
            var result = await _LeasesubpurposeRepository.FindBy(a => a.Id == id);
            Leasesubpurpose model = result.FirstOrDefault();
            model.PurposeUseId = leasesubpurpose.PurposeUseId;
            model.SubPurposeUse = leasesubpurpose.SubPurposeUse;
            model.IsActive = leasesubpurpose.IsActive;
            model.ModifiedDate = DateTime.Now;
            _LeasesubpurposeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Leasesubpurpose Leasesubpurpose)
        {
         
            Leasesubpurpose.CreatedDate = DateTime.Now;
            _LeasesubpurposeRepository.Add(Leasesubpurpose);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string SubPurposeUse, int PurposeUseId)
        {
            bool result = await _LeasesubpurposeRepository.Any(id, SubPurposeUse, PurposeUseId);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _LeasesubpurposeRepository.FindBy(a => a.Id == id);
            Leasesubpurpose model = form.FirstOrDefault();
            model.IsActive = 0;
            _LeasesubpurposeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Leasepurpose>> GetPurposeUseList()
        {
            return await _LeasesubpurposeRepository.GetPurposeUseList();
        }

        public async Task<PagedResult<Leasesubpurpose>> GetPagedLeasesubpurpose(LeasesubpurposeSearchDto model)
        {
            return await _LeasesubpurposeRepository.GetPagedLeasesubpurpose(model);
        }



        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _LeasesubpurposeRepository.GetAllLeasepurpose();
            return leasePurposeList;
        }


        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose()
        {
            return await _LeasesubpurposeRepository.GetAllLeaseSubpurpose();
        }
    }
}
