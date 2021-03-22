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

    public class LeaseHearingDetailsService : EntityService<Requestforproceeding>, ILeaseHearingDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaseHearingDetailsRepository _leaseHearingDetailsRepository;
        private readonly IMapper _mapper;
        public LeaseHearingDetailsService(IUnitOfWork unitOfWork,
            ILeaseHearingDetailsRepository leaseHearingDetailsRepository,
            IMapper mapper)
        : base(unitOfWork, leaseHearingDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _leaseHearingDetailsRepository = leaseHearingDetailsRepository;
            _mapper = mapper;
        }

       
    }
}
