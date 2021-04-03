using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface ILeasePaymentTypeService : IEntityService<Leasepaymenttype>
    {
        Task<List<Leasepaymenttype>> GetAllLeasepaymenttype();

        Task<bool> Update(int id, Leasepaymenttype rent);
        Task<bool> Create(Leasepaymenttype rate);
        Task<Leasepaymenttype> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Leasepaymenttype>> GetPagedLeasepaymenttype(LeasePaymentTypeSearchDto model);

    }
}
