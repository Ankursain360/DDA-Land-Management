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
    public interface INewlandus6plotService : IEntityService<Newlandus6plot>
    {
        Task<bool> Update(int id, Newlandus6plot us6);
        Task<bool> Create(Newlandus6plot us6);
        Task<Newlandus6plot> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandus6plot>> GetPagedUS6Plot(Newlandus6plotSearchDto model);
        Task<List<Newlandus6plot>> GetAllUS6Plot();
        Task<List<LandNotification>> GetAllNotification();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        //Task<List<Newlandus6plot>> GetAllFetchNotification6Details(int? NotificationId);
        Task<PagedResult<Newlandus6plot>> GetAllFetchNotificationDetails(NewLandNotification6ListSearchDto model);
    }
}
