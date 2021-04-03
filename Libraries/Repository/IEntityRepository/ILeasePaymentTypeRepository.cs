using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeasePaymentTypeRepository : IGenericRepository<Leasepaymenttype>
    {
        Task<PagedResult<Leasepaymenttype>> GetPagedLeasepaymenttype(LeasePaymentTypeSearchDto model);
        Task<List<Leasepaymenttype>> GetAllLeasepaymenttype();
    }
}
