using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
  public  interface IGramsabhalandRepository : IGenericRepository<Gramsabhaland>
    {

        Task<List<Gramsabhaland>> GetAllGramsabhaland();
        Task<List<Gramsabhaland>> GetAllGramsabhalandList(GramsabhalandSearchDto model);
        Task<List<Zone>> GetAllZone();
        Task<List<Acquiredlandvillage>> GetAllVillage(int? zoneId);
        Task<PagedResult<Gramsabhaland>> GetPagedGramsabhaland(GramsabhalandSearchDto model);
    }
}
