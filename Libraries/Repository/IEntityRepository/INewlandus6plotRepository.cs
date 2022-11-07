using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
   public interface INewlandus6plotRepository : IGenericRepository<Newlandus6plot>
    {
        Task<PagedResult<Newlandus6plot>> GetPagedUS6Plot(Newlandus6plotSearchDto model);
        Task<List<Newlandus6plot>> GetAllUS6Plot();
        Task<List<Newlandus6plot>> GetAllUS6PlotList(Newlandus6plotSearchDto model);
        Task<List<Newlandnotification>> GetAllNotification();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        //Task<List<Newlandus6plot>> GetAllFetchNotification6Details(int? NotificationId);
        Task<PagedResult<Newlandus6plot>> GetAllFetchNotificationDetails(NewLandNotification6ListSearchDto model);
    }
}
