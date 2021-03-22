using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{
    public interface ILeasepaymentdetailsService
    {

        Task<bool> Delete(int id);
        Task<Leasepaymentdetails> FetchSingleResult(int id);
        Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? appId)
;
        Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
;
        Task<List<Allotmententry>> GetAllAllotmententry();
        Task<List<Leaseapplication>> GetAllLeaseApplication()
;
        Task<List<Leasepaymentdetails>> GetAllLeasepaymentdetails();
        Task<bool> Update(int id, Leasepaymentdetails Leasepaymentdetails);
        Task<bool> Create(Leasepaymentdetails Leasepaymentdetails);
        Task<PagedResult<Leasepaymentdetails>> GetPagedLeasepaymentdetails(LeasepaymentdetailsSearchDto model);
        Task<List<Leasepaymenttype>> BindAllPaymentType();


    }

}
