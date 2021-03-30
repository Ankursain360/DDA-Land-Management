

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

namespace Libraries.Service.ApplicationService
{

    public class ApplyForServicesService : EntityService<Servicetype>, IApplyForServicesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplyForServicesRepository _applyForServicesRepository;

        public ApplyForServicesService(IUnitOfWork unitOfWork, IApplyForServicesRepository applyForServicesRepository)
        : base(unitOfWork, applyForServicesRepository)
        {
            _unitOfWork = unitOfWork;
            _applyForServicesRepository = applyForServicesRepository;
        }

        public async Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceSearchDto model)
        {
            return await _applyForServicesRepository.GetPagedServicetype(model);
        }


    }
}

