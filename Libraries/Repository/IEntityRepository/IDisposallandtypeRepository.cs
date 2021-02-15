using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
  
      public interface IDisposallandtypeRepository : IGenericRepository<Disposallandtype>
    {
        Task<List<Disposallandtype>> GetDisposallandtype();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Disposallandtype>> GetPagedDisposalLandType(DisposalLandTypeSearchDto model);


    }
}
