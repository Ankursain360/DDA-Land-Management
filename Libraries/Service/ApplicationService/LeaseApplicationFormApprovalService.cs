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

    public class LeaseApplicationFormApprovalService : EntityService<Leaseapplication>, ILeaseApplicationFormApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaseApplicationFormApprovalRepository _leaseApplicationApprovalRepository;
        private readonly IMapper _mapper;
        public LeaseApplicationFormApprovalService(IUnitOfWork unitOfWork,
            ILeaseApplicationFormApprovalRepository leaseApplicationApprovalRepository,
            IMapper mapper)
        : base(unitOfWork, leaseApplicationApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _leaseApplicationApprovalRepository = leaseApplicationApprovalRepository;
            _mapper = mapper;
        }

        public async Task<Leaseapplication> FetchSingleResult(int id)
        {
            return await _leaseApplicationApprovalRepository.FetchSingleResult(id);
        }

        public async Task<PagedResult<Leaseapplication>> GetPagedLeaseApplicationFormDetails(LeaseApplicationFormApprovalSearchDto model, int userId)
        {
            return await _leaseApplicationApprovalRepository.GetPagedLeaseApplicationFormDetails( model,  userId);
        }
    }
}
