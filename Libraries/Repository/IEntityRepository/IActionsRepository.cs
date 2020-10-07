using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IActionsRepository : IGenericRepository<Actions>
    {
        Task<PagedResult<Actions>> GetPagedActions(ActionsSearchDto model);

        Task<bool> Any(int id, string name);
    }
}