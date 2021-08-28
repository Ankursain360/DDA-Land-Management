using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Libraries.Repository.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDemandDetailsService
    {
        Task<PagedResult<Kycform>> GetPagedDemandDetails(DemandDetailsSearchDto model, string MobileNo);

        Task<PagedResult<Kycform>> GetDemandPaymentDetails(DemandDetailsSearchDto model, string MobileNo);

        Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int Id);
      

        Task<bool> Create(Kycdemandpaymentdetails kycDemandPayment);

        Task<bool> UpdateBeforeApproval(int id, Kycdemandpaymentdetails kycdemandpaymentdetails);


        Task<List<LeasePaymentDemandLetterDetailsSearchDto>> GetPaymentDemandLetter(int Id);

        Task<List<Kycdemandpaymentdetails>> FetchResultOnKycId(int kycId);//added by ishu
        Task<bool> RollBackEntry(int id);//added by ishu
    }
}
