using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{

    public interface ILeasepaymentdetailsRepository : IGenericRepository<Leasepaymentdetails>
    {
        Task<List<Allotmententry>> GetAllAllotmententry();
        Task<List<Leasepaymentdetails>> GetAllLeasepaymentdetails();
        Task<List<Leaseapplication>> GetAllLeaseApplication();
        Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId);
        Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? AppId);
        Task<PagedResult<Leasepaymentdetails>> GetPagedLeasepaymentdetails(LeasepaymentdetailsSearchDto model);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<List<Leasepaymenttype>> GetAllPaymentType();

    }
}
