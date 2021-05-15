using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IWatchAndWardApprovalService : IEntityService<Watchandward>
    {
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Khasra>> GetAllKhasra();
        Task<List<Village>> GetAllVillage();
        Task<List<Locality>> GetAllLocality();
        Task<Watchandward> FetchSingleResult(int id);
        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardApprovalSearchDto model, int userId, int zoneId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
