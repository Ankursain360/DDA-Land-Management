using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
   public interface ICasenatureRepository : IGenericRepository<Casenature>
    {
        Task<PagedResult<Casenature>> GetPagedcasenature(CasenatureSearchDto model);
        Task<List<Casenature>> Getcasenature();
        
        Task<bool> Any(int id, string casenature);
    }
}
