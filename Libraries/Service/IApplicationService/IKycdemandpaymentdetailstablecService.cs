﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IKycdemandpaymentdetailstablecService
    {
        Task<bool> SaveKycChallanDetails(Kycdemandpaymentdetailstablec kycDemandPayment);
        Task<Kycdemandpaymentdetailstablec> FetchSingleResult(int id);
    }
}
