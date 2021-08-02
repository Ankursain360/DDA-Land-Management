using System;



using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IKycPaymentApprovalRepository : IGenericRepository<Kycdemandpaymentdetails>
    {
        Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid);
        Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId);
        Task<Kycdemandpaymentdetails> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);

        //********* rpt ! Allottee challan  Details  repeter**********
        Task<bool> SaveChallan(Kycdemandpaymentdetailstablec challan);
        Task<List<Kycdemandpaymentdetailstablec>> GetAllChallan(int id);
        Task<bool> DeleteChallan(int Id);

        //approval related 
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);

    }
}
