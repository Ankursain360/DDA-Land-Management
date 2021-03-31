using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeasesubpurposeRepository : IGenericRepository<Leasesubpurpose>
    {

        Task<bool> Any(int id, string SubPurposeUse, int PurposeUseId);
        Task<List<Leasepurpose>> GetPurposeUseList();
        Task<PagedResult<Leasesubpurpose>> GetPagedLeasesubpurpose(LeasesubpurposeSearchDto model);
        Task<Leasesubpurpose> FetchSingleResult(int id);

       Task<List<Leasepurpose>> GetAllLeasepurpose();

        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose();
    }
}