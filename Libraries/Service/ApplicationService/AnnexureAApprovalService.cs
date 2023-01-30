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
    public class AnnexureAApprovalService : EntityService<Fixingdemolition>, IAnnexureAApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnexureAApprovalRepository _annexureAApprovalRepository;
        public AnnexureAApprovalService(IUnitOfWork unitOfWork, IAnnexureAApprovalRepository annexureAApprovalRepository)
        : base(unitOfWork, annexureAApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _annexureAApprovalRepository = annexureAApprovalRepository;
        }

        public async Task<Fixingdemolition> FetchSingleResult(int id)
        {
            return await _annexureAApprovalRepository.FetchSingleResult(id);
        }
        public async Task<List<Fixingdemolition>> GetAllFixingdemolition(AnnexureAApprovalSearchDto model, int userId, int zoneId)
        {
            return await _annexureAApprovalRepository.GetAllFixingdemolition(model,userId,zoneId);
        }
        public async Task<PagedResult<Fixingdemolition>> GetPagedAnnexureA(AnnexureAApprovalSearchDto model, int userId, int zoneId)
        {
            return await _annexureAApprovalRepository.GetPagedAnnexureA(model, userId, zoneId);
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _annexureAApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
    }
}
