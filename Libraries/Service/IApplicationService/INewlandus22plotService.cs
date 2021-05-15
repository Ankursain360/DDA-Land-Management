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
    public interface INewlandus22plotService : IEntityService<Newlandus22plot>
    {
        Task<bool> Update(int id, Newlandus22plot us22);
        Task<bool> Create(Newlandus22plot us22);
        Task<Newlandus22plot> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandus22plot>> GetPagedUS22Plot(Newlandus22plotSearchDto model);
        Task<List<Newlandus22plot>> GetAllUS22Plot();
        Task<List<Newlandus4plot>> GetAllUS4Plot(int? notificationId);
        Task<List<Newlandus6plot>> GetAllUS6Plot(int? notificationId);
        Task<List<Newlandus17plot>> GetAllUS17Plot(int? notificationId);
        Task<Newlandus4plot> FetchUS4Plot(int? notificationId);
        Task<Newlandus6plot> FetchUS6Plot(int? notificationId);
        Task<Newlandus17plot> FetchUS17Plot(int? notificationId);
        Task<List<Newlandnotification>> GetAllNotification();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Newlandus22plot>> GetAllFetchNotificationDetails(NewLandNotification22ListSearchDto model);
    }
}
