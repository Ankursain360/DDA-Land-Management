using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Libraries.Service.IApplicationService
{
    public interface INewlandnotificationService
    {
        Task<List<NewlandNotificationtype>> GetNotificationType();
        Task<bool> Delete(int id);
        Task<bool> Create(Newlandnotification newlandnotification);
        Task<bool> Update(int id, Newlandnotification newlandnotification);
        Task<Newlandnotification> FetchSingleResult(int id);
        Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model);
        Task<List<Newlandnotification>> GetNewlandnotification();


        }
}
