//using System;
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

    public interface ILicenceFeesService : IEntityService<Licencefees>
    {
        Task<List<Licencefees>> GetAllLicencefees();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId);
        Task<bool> Update(int id, Licencefees licencefees);
        Task<bool> Create(Licencefees licencefees);
        Task<Licencefees> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Licencefees>> GetPagedLicencefees(LicencefeesSearchDto model);

    }
}
