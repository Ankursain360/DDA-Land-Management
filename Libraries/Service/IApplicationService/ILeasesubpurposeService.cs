using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface ILeasesubpurposeService : IEntityService<Leasesubpurpose>
    {
        Task<bool> Update(int id, Leasesubpurpose leasesubpurpose); 

        Task<bool> Create(Leasesubpurpose Leasesubpurpose);

        Task<Leasesubpurpose> FetchSingleResult(int id); 

        Task<bool> Delete(int id);   

        Task<bool> CheckUniqueName(int id, string SubPurposeUse, int PurposeUseId);
        Task<List<Leasepurpose>> GetPurposeUseList();
        Task<PagedResult<Leasesubpurpose>> GetPagedLeasesubpurpose(LeasesubpurposeSearchDto model);

        Task<List<Leasepurpose>> GetAllLeasepurpose();

        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose();
    }
}
