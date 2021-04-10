using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IApplicationNotificationTemplateRepository : IGenericRepository<ApplicationNotificationTemplate>
    {
        Task<PagedResult<ApplicationNotificationTemplate>> GetPagedTemplate(ApplicationNotificationTemplateSearchDto model);

        Task<bool> Any(int id, string name);
    }
}
