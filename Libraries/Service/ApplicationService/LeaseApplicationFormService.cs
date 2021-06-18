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

    public class LeaseApplicationFormService : EntityService<Leaseapplication>, ILeaseApplicationFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaseApplicationFormRepository _leaseApplicationRepository;
        private readonly IMapper _mapper;
        public LeaseApplicationFormService(IUnitOfWork unitOfWork,
            ILeaseApplicationFormRepository leaseApplicationRepository,
            IMapper mapper)
        : base(unitOfWork, leaseApplicationRepository)
        {
            _unitOfWork = unitOfWork;
            _leaseApplicationRepository = leaseApplicationRepository;
            _mapper = mapper;
        }
        public async Task<bool> Create(Leaseapplication leaseapplication)
        {
            leaseapplication.CreatedDate = DateTime.Now;
            _leaseApplicationRepository.Add(leaseapplication);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Leaseapplication> FetchLeaseApplicationDetails(int id)
        {
            return await _leaseApplicationRepository.FetchLeaseApplicationDetails(id);
        }
        public async Task<Allotmentletter> FetchLeaseApplicationDetailsforAllotmentLetter(int id)
        {
            return await _leaseApplicationRepository.FetchLeaseApplicationDetailsforAllotmentLetter(id);
        }
        public async Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid)
        {
            return await _leaseApplicationRepository.GetDocumentChecklistDetails(servicetypeid);
        }

        public async Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id)
        {
            return await _leaseApplicationRepository.LeaseApplicationDocumentDetails(id);
        }

        public async Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments)
        {
            leaseapplicationdocuments.ForEach(x => x.CreatedDate = DateTime.Now);
            return await _leaseApplicationRepository.SaveLeaseApplicationDocuments(leaseapplicationdocuments);
        }
        public async Task<PagedResult<Leaseapplication>> GetPagedAllotmentLetter(DocumentChecklistSearchDto model)
        {
            return await _leaseApplicationRepository.GetPagedAllotmentLetter(model);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Leaseapplication leaseapplication)
        {
            var result = await _leaseApplicationRepository.FindBy(a => a.Id == id);
            Leaseapplication model = result.FirstOrDefault();

            model.ApprovedStatus = leaseapplication.ApprovedStatus;
            model.PendingAt = leaseapplication.PendingAt;
            _leaseApplicationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Leaseapplicationdocuments> FetchLeaseApplicationDocumentDetails(int id)
        {
            return await _leaseApplicationRepository.FetchLeaseApplicationDocumentDetails(id);
        }
        public async Task<List<Allotmententry>> GetRefNoListforAllotmentLetter()
        {
            return await _leaseApplicationRepository.GetRefNoListforAllotmentLetter();
        }

        public async Task<bool> RollBackEntry(int id)
        {
            var result = await _leaseApplicationRepository.FindBy(a => a.Id == id);
            Leaseapplication model = result.FirstOrDefault();
            if (model != null)
            {
                _leaseApplicationRepository.Delete(model);
                return await _unitOfWork.CommitAsync() > 0;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> RollBackEntryDocument(int id)
        {
            return await _leaseApplicationRepository.RollBackEntryDocument(id);
        }
    }
}
