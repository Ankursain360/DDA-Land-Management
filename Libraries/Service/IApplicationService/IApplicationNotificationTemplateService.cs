using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IApplicationNotificationTemplateService
    {
        Task<List<ApplicationNotificationTemplate>> GetAllTemplate();
        Task<bool> Update(int id, ApplicationNotificationTemplate template);

        Task<bool> Create(ApplicationNotificationTemplate template);

        Task<ApplicationNotificationTemplate> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string actions);

        Task<PagedResult<ApplicationNotificationTemplate>> GetPagedTemplate(ApplicationNotificationTemplateSearchDto model);
    }
}
