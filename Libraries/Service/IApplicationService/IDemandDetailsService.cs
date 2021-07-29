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

         Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int Id);
        Task<Kycform> FetchSingleResult(int Id);


        
    }
}
