using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Libraries.Service.IApplicationService
{

    public interface INewlandnotificationService : IEntityService<Newlandnotification>
    {
        Task<List<NewlandNotificationtype>> GetNotificationType();
        Task<bool> Delete(int id);
        Task<bool> Create(Newlandnotification newlandnotification);
        Task<bool> Update(int id, Newlandnotification newlandnotification);
        Task<Newlandnotification> FetchSingleResult(int id);
        Task<Newlandnotification> FetchSingleResult1(int id);
        Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model);
        Task<List<Newlandnotification>> GetNewlandnotification();

        Task<bool> DeleteNewlandnotification(int Id);
        Task<bool> Deletefiledetails(int Id);
        
        Task<bool> Any(int id, string name);
        Task<List<Newlandnotification>> GetAllNewlandNotification();
        Task<List<NewlandNotificationtype>> GetAllNotificationType();
        Task<List<Newlandnotificationfilepath>> GetAllfiledetails(int Id);
        Task<bool> SaveNewlandNotification(Newlandnotification newlandnotification);
        Task<bool> Savefiledetails(Newlandnotificationfilepath newlandnotificationfilepath);
        Task<Newlandnotification> NewLandNotificationFile(int Id);

    }
}
