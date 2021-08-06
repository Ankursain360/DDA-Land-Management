

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

        public async Task<Kycdemandpaymentdetails> FetchSingleResult(int id)
        {
            return await _kycPaymentApprovalRepository.FetchSingleResult(id);
        }
        public async Task<bool> UpdatePaymentDetails(int id, DemandPaymentDetailsDto dto)
        {
            var result = await _kycPaymentApprovalRepository.FindBy(a => a.Id == id);
            Kycdemandpaymentdetails model = result.FirstOrDefault();
            model.TotalPayable = dto.GroundRentLeaseRent;
            model.TotalPayableInterest = dto.InterestAmount;
            model.TotalDues = dto.TotalDues;
           // model.TotalPayable = dto.Name;

            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _kycPaymentApprovalRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> UpdateBeforeApproval(int id, Kycdemandpaymentdetails payment)
        {
            var result = await _kycPaymentApprovalRepository.FindBy(a => a.Id == id);
            Kycdemandpaymentdetails model = result.FirstOrDefault();

            model.ApprovedStatus = payment.ApprovedStatus;
            model.PendingAt = payment.PendingAt;
            _kycPaymentApprovalRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _kycPaymentApprovalRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
        public async Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId)
        {
            var data = await _kycPaymentApprovalRepository.GetPagedKycPaymentDetails(model, userId);
            return data;
        }


        //********* rpt ! Allottee challan  Details  repeter**********

        public async Task<bool> SaveChallan(List<Kycdemandpaymentdetailstablec> challan)
        {
            challan.ForEach(x => x.CreatedBy = 1);
            challan.ForEach(x => x.CreatedDate = DateTime.Now);
            challan.ForEach(x => x.IsActive = 1);
            return await _kycPaymentApprovalRepository.SaveChallan(challan);
        }
        public async Task<List<Kycdemandpaymentdetailstablec>> GetAllChallan(int id)
        {
            return await _kycPaymentApprovalRepository.GetAllChallan(id);
        }
        public async Task<bool> DeleteChallan(int Id)
        {
            return await _kycPaymentApprovalRepository.DeleteChallan(Id);
        }

        // approval related
        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _kycPaymentApprovalRepository.FetchSingleResultOnProcessGuid(processguid);
        }


        //Payment rpt 

        public async Task<bool> SavePayment(List<Kycdemandpaymentdetailstablea> Payment)
        {

            Payment.ForEach(x => x.CreatedBy = 1);
            Payment.ForEach(x => x.CreatedDate = DateTime.Now);
            Payment.ForEach(x => x.IsActive = 1);
           
            return await _kycPaymentApprovalRepository.SavePayment(Payment);
        }
        public async Task<List<Kycdemandpaymentdetailstablea>> GetAllPayment(int id)
        {
            return await _kycPaymentApprovalRepository.GetAllPayment(id);
        }
        public async Task<bool> DeletePayment(int Id)
        {
            return await _kycPaymentApprovalRepository.DeletePayment(Id);
        }

    }
}
//Kycdemandpaymentdetailstablea