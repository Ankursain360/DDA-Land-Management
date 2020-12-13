using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemolitionPoliceAssistenceLetterRepository : IGenericRepository<Fixingdemolition>
    {
        Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId);
    }
}