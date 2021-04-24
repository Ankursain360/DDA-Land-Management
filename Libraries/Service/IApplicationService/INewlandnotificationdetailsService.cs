
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
    public interface INewlandnotificationdetailsService : IEntityService<Newlandnotificationdetails>
    {
       
        Task<bool> Update(int id, Newlandnotificationdetails notification);
        Task<bool> Create(Newlandnotificationdetails notification);
        Task<Newlandnotificationdetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<List<NewlandNotificationtype>> GetAllNotificationType();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);

        Task<List<Newlandnotificationdetails>> GetAllNotifications();
        Task<PagedResult<Newlandnotificationdetails>> GetPagedNotifications(NewlandnotificationdetailsSearchDto model);


    }
}
