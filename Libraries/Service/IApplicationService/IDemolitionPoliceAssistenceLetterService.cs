using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDemolitionPoliceAssistenceLetterService : IEntityService<Demolitionpoliceassistenceletter>
    {
        Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId);
        Task<bool> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter);
    }
}
