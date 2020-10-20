using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IWatchAndWardApprovalRepository:IGenericRepository<Watchandward>
    {
        Task<List<Watchandward>> GetWatchandward();
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Village>> GetAllVillage();
        Task<List<Locality>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model);
    }
}
