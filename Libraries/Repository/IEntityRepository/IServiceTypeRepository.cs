using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IServiceTypeRepository : IGenericRepository<Servicetype>
    {
        Task<List<Servicetype>> GetServicetype();

        Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceTypeSearchDto model);
    }
}

