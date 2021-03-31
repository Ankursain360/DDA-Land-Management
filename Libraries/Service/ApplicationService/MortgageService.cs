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

    public class MortgageService : EntityService<Mortgage>, IMortgageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMortgageRepository _mortgageRepository;
        private readonly IMapper _mapper;
        public MortgageService(IUnitOfWork unitOfWork,
            IMortgageRepository mortgageRepository,
            IMapper mapper)
        : base(unitOfWork, mortgageRepository)
        {
            _unitOfWork = unitOfWork;
            _mortgageRepository = mortgageRepository;
            _mapper = mapper;
        }
    }
}
