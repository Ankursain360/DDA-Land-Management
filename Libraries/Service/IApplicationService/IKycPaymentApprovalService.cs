using System;
using System.Collections.Generic;
using System.Text;


using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IKycPaymentApprovalService : IEntityService<Kycdemandpaymentdetails>
    {
        Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid);
        Task<Kycdemandpaymentdetails> FetchSingleResult(int id);
        Task<bool> UpdatePaymentDetails(int id, DemandPaymentDetailsDto dto);
        Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<bool> UpdateBeforeApproval(int id, Kycdemandpaymentdetails payment);

        //********* rpt ! Allottee challan  Details  repeter**********
        Task<bool> SaveChallan(List<Kycdemandpaymentdetailstablec> challan);
        Task<List<Kycdemandpaymentdetailstablec>> GetAllChallan(int id);
        Task<bool> DeleteChallan(int Id);

        // approval related
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);

        //payment rpt 
        Task<List<Kycdemandpaymentdetailstablea>> GetAllPayment(int id);
        Task<bool> DeletePayment(int Id);
        Task<bool> SavePayment(List<Kycdemandpaymentdetailstablea> Payment);
    }
}
