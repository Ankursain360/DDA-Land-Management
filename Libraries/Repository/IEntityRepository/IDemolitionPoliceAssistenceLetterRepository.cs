using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemolitionPoliceAssistenceLetterRepository : IGenericRepository<Demolitionpoliceassistenceletter>
    {
        Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId);
        Task<PagedResult<Demolitionpoliceassistenceletter>> GetPagedApprovedAnnexureAListedit(DemolitionPoliceAssistenceLetterSearchDto model, int userId);
        Task<Demolitionpoliceassistenceletter> FetchSingleResult(int id);
        Task<Demolitionpoliceassistenceletter> FetchSingleResultButOnAneexureId(int id);
    }
}