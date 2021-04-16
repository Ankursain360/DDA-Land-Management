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
        public interface INewlandus4plotService : IEntityService<Newlandus4plot>
        {
             Task<bool> Update(int id, Newlandus4plot us4);
             Task<bool> Create(Newlandus4plot us4);
             Task<Newlandus4plot> FetchSingleResult(int id);
             Task<bool> Delete(int id);
             Task<PagedResult<Newlandus4plot>> GetPagedUS4Plot(Newlandus4plotSearchDto model);
             Task<List<Newlandus4plot>> GetAllUS4Plot();
             Task<List<LandNotification>> GetAllNotification();
             Task<List<Newlandvillage>> GetAllVillage();
             Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
             Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<Newlandkhasra> FetchSingleKhasra1Result(int? khasraId);
        Task<List<Newlandus4plot>> GetAllFetchNotificationDetails(int? NotificationId);
    }
}
