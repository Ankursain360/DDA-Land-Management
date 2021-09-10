

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class KycformApprovalService : EntityService<Kycform>, IKycformApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycformApprovalRepository _kycformApprovalRepository;

        public KycformApprovalService(IUnitOfWork unitOfWork, IKycformApprovalRepository kycformApprovalRepository)
        : base(unitOfWork, kycformApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _kycformApprovalRepository = kycformApprovalRepository;

        }

        public async Task<PagedResult<Kycform>> GetPagedKycFormDetails(KycFormApprovalSearchDto model, int userId,int? BranchId)
        {
            var data = await _kycformApprovalRepository.GetPagedKycFormDetails(model, userId, BranchId);
            return data;
        }

        public async Task<Kycform> FetchSingleResult(int id)
        {
            return await _kycformApprovalRepository.FetchSingleResult(id);
        }
        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version)
        {
            return await _kycformApprovalRepository.FetchSingleResultOnProcessGuidWithVersion(processguid, version);
        }

        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _kycformApprovalRepository.FetchSingleResultOnProcessGuid(processguid);
        }


        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _kycformApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
    }
}
