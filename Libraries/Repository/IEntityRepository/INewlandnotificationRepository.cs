using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandnotificationRepository : IGenericRepository<Newlandnotification>
    { 
        Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model);
        Task<List<NewlandNotificationtype>> GetNotificationType();
        Task<List<Newlandnotification>> GetNewlandnotificationdetails();
        Task<bool> Any(int id, string name);
    }
}
