using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IKycdemandpaymentdetailstableaService
    {
        Task<Kycdemandpaymentdetailstablea> FetchSingleResult(int id);
        Task<Kycdemandpaymentdetailstablea> FetchSingleResultonDemandId(int id);
        Task<bool> Update(int id, Kycdemandpaymentdetailstablea payment);
        Task<List<Kycdemandpaymentdetailstablea>> FetchResultOnDemandId(int id);
        Task<bool> SaveDemandPaymentDetails(Kycdemandpaymentdetailstablea kycDemandPayment);
    }
}