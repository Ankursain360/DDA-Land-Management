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
    public class EncroachmentRegisterationApprovalService : EntityService<EncroachmentRegisteration>, IEncroachmentRegisterationApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncroachmentRegisterationApprovalRepository _encroachmentRegisterationApprovalRepository;
        public EncroachmentRegisterationApprovalService(IUnitOfWork unitOfWork, IEncroachmentRegisterationApprovalRepository encroachmentRegisterationApprovalRepository)
        : base(unitOfWork, encroachmentRegisterationApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _encroachmentRegisterationApprovalRepository = encroachmentRegisterationApprovalRepository;
        }

        public async Task<EncroachmentRegisteration> FetchSingleResult(int id)
        {
            var result = await _encroachmentRegisterationApprovalRepository.FindBy(a => a.Id == id);
            EncroachmentRegisteration model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _encroachmentRegisterationApprovalRepository.GetAllKhasra();
        }

        public async Task<List<Locality>> GetAllLocality()
        {
            return await _encroachmentRegisterationApprovalRepository.GetAllLocality();
        }
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _encroachmentRegisterationApprovalRepository.GetAllEncroachmentRegisteration();
        }

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterApprovalSearchDto model, int userId,int zoneId)
        {
            return await _encroachmentRegisterationApprovalRepository.GetPagedEncroachmentRegisteration(model, userId, zoneId);
        }
        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _encroachmentRegisterationApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
    }
}
