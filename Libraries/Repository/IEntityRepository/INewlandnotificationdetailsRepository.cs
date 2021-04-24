
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandnotificationdetailsRepository : IGenericRepository<Newlandnotificationdetails>
    {

        Task<List<NewlandNotificationtype>> GetAllNotificationType();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);

        Task<List<Newlandnotificationdetails>> GetAllNotifications();
        Task<PagedResult<Newlandnotificationdetails>> GetPagedNotifications(NewlandnotificationdetailsSearchDto model);
    }
}
