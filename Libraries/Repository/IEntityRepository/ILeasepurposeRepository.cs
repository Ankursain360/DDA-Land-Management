using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeasepurposeRepository : IGenericRepository<Leasepurpose>
    {
        Task<List<Leasepurpose>> GetLeasepurposes();

        Task<PagedResult<Leasepurpose>> GetpagedLeasepurpose(LeasepurposeSearchDto model);
    }
}

