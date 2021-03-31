

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IApplyForServicesRepository : IGenericRepository<Servicetype>
    {
      
        Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceSearchDto model);
      

    }
}
