using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IGramsabhalandService
    {

        Task<List<Gramsabhaland>> GetAllGramsabhaland();
        Task<List<Zone>> GetAllZone();
        Task<List<Acquiredlandvillage>> GetAllVillage(int? zoneId);
        Task<PagedResult<Gramsabhaland>> GetPagedGramsabhaland(GramsabhalandSearchDto model);

        Task<bool> Update(int id, Gramsabhaland gramsabhaland);
        Task<bool> Create(Gramsabhaland gramsabhaland);

        Task<Gramsabhaland> FetchSingleResult(int id);
        Task<bool> Delete(int id);


    }
}
