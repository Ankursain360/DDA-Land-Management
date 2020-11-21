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
        Task<bool> Update(int id, Watchandward watchandward);
        Task<bool> Create(Watchandward watchandward);
        Task<Watchandward> FetchSingleResult(int id);
        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model, int userId);
    }
}
