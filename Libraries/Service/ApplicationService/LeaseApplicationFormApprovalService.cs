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
        private readonly ILeaseApplicationFormRepository _leaseApplicationApprovalRepository;
        private readonly IMapper _mapper;
        public LeaseApplicationFormApprovalService(IUnitOfWork unitOfWork,
            ILeaseApplicationFormRepository leaseApplicationApprovalRepository,
            IMapper mapper)
        : base(unitOfWork, leaseApplicationApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _leaseApplicationApprovalRepository = leaseApplicationApprovalRepository;
            _mapper = mapper;
        }
    }
}
