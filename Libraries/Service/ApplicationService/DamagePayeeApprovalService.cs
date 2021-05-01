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

namespace Service.ApplicationService
{
  
    public class DamagePayeeApprovalService : EntityService<Damagepayeeregister>, IDamagePayeeApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamagePayeeApprovalRepository _damagePayeeApprovalRepository;


        public DamagePayeeApprovalService(IUnitOfWork unitOfWork, IDamagePayeeApprovalRepository damagePayeeApprovalRepository)
        : base(unitOfWork, damagePayeeApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _damagePayeeApprovalRepository = damagePayeeApprovalRepository;
        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamageForApproval(DamagepayeeRegisterApprovalDto model, int userId)
        {
            return await _damagePayeeApprovalRepository.GetPagedDamageForApproval(model, userId);
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _damagePayeeApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }

        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            return await _damagePayeeApprovalRepository.FetchSingleResult(id);
        }
    }
}
