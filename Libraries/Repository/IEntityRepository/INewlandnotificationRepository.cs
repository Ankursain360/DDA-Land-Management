using Dto.Search;
using Libraries.Model.Common;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandnotificationRepository : IGenericRepository<Newlandnotification>
    { 
        Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model);
        Task<List<NewlandNotificationtype>> GetNotificationType();
       // Task<List<Newlandnotification>> GetNewlandnotificationdetails();
        Task<bool> DeleteNewlandnotification(int Id);
        Task<bool> Deletefiledetails(int Id);
        Task<Newlandnotification> FetchSingleResult(int id);
        Task<bool> Any(int id, string name);
        Task<List<Newlandnotification>> GetAllNewlandNotification();
        Task<List<NewlandNotificationtype>> GetAllNotificationType();
        Task<List<Newlandnotificationfilepath>> GetAllfiledetails(int Id);
        Task<bool> SaveNewlandNotification(Newlandnotification newlandnotification);
        Task<bool> Savefiledetails(Newlandnotificationfilepath newlandnotificationfilepath);
    }
}
