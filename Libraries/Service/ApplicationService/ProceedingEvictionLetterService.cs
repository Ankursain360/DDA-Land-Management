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

    public class ProceedingEvictionLetterService : EntityService<Leaseapplication>, IProceedingEvictionLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProceedingEvictionLetterRepository _proceedingEvictionLetterRepository;
        private readonly IMapper _mapper;
        public ProceedingEvictionLetterService(IUnitOfWork unitOfWork,
            IProceedingEvictionLetterRepository proceedingEvictionLetterRepository,
            IMapper mapper)
        : base(unitOfWork, proceedingEvictionLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _proceedingEvictionLetterRepository = proceedingEvictionLetterRepository;
            _mapper = mapper;
        }
    }
}
