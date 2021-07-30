using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{ 
    public interface IKycdemandpaymentdetailstablebService
    {
        Task<bool> SaveDemandPaymentAPIDetails(Kycdemandpaymentdetailstableb kycDemandPayment);

    }
}
