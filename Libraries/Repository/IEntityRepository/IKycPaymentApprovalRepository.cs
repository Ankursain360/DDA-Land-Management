using System;



using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Libraries.Repository.IEntityRepository
{

    public interface IKycPaymentApprovalRepository : IGenericRepository<Kycdemandpaymentdetails>
    {
        Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid);
        Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId);
        Task<Kycdemandpaymentdetails> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
        Task<Userprofile> FetchDDofBranch(int? BranchId);//DD info of particular branch to display in outstanding dues mail

        //********* rpt ! Allottee challan  Details  repeter**********
        Task<bool> SaveChallan(List<Kycdemandpaymentdetailstablec> challan);
        Task<List<Kycdemandpaymentdetailstablec>> GetAllChallan(int id);
        Task<bool> DeleteChallan(int Id);

        //approval related 
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);

        //payment rpt

        Task<List<Kycdemandpaymentdetailstablea>> GetAllPayment(int id);
        Task<bool> DeletePayment(int Id);

        Task<bool> SavePayment(List<Kycdemandpaymentdetailstablea> Payment);


    }
}
