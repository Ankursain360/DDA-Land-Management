

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

    public class ExtensionApprovalService : EntityService<Extension>, IExtensionApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExtensionApprovalRepository _extensionApprovalRepository;
        private readonly IMapper _mapper;
        public ExtensionApprovalService(IUnitOfWork unitOfWork,
            IExtensionApprovalRepository extensionApprovalRepository,
            IMapper mapper)
        : base(unitOfWork, extensionApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _extensionApprovalRepository = extensionApprovalRepository;
            _mapper = mapper;
        }

        public async Task<Extension> FetchSingleResult(int id)
        {
            return await _extensionApprovalRepository.FetchSingleResult(id);
        }

        public async Task<PagedResult<Extension>> GetPagedExtensionDetails(ExtensionApprovalSearchDto model, int userId)
        {
            return await _extensionApprovalRepository.GetPagedExtensionDetails(model, userId);
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _extensionApprovalRepository.IsApplicationPendingAtUserEnd( id,  userId);
        }
    }
}
