using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
   public interface INewLandNotificationTypeService : IEntityService<NewlandNotificationtype>
    {
        Task<PagedResult<NewlandNotificationtype>> GetPagedNotification(NewLandNotificationTypeSearchDto model);

        Task<bool> Create(NewlandNotificationtype notification);

        Task<List<NewlandNotificationtype>> GetAllNotificationType();


        Task<NewlandNotificationtype> FetchSingleResult(int id); 

        Task<bool> Update(int id, NewlandNotificationtype notification);

        Task<bool> Delete(int id);
    }
}
