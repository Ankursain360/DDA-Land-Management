using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewLandNotificationTypeRepository : IGenericRepository<NewlandNotificationtype>
    {

        Task<PagedResult<NewlandNotificationtype>> GetPagedZone(NewLandNotificationTypeSearchDto model);

    }
}
