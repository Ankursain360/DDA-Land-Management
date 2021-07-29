

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
    public class KycPaymentApprovalService : EntityService<Kycdemandpaymentdetails>, IKycPaymentApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycPaymentApprovalRepository _kycPaymentApprovalRepository;

        public KycPaymentApprovalService(IUnitOfWork unitOfWork, IKycPaymentApprovalRepository kycPaymentApprovalRepository)
        : base(unitOfWork, kycPaymentApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _kycPaymentApprovalRepository = kycPaymentApprovalRepository;

        }
        public async Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _kycPaymentApprovalRepository.GetWorkFlowDataOnGuid(processguid);
        }




        public async Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId)
        {
            var data = await _kycPaymentApprovalRepository.GetPagedKycPaymentDetails(model, userId);
            return data;
        }


    }
}
