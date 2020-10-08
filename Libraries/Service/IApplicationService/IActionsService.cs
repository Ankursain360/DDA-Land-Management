using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IActionsService : IEntityService<Actions>
    {
        Task<List<Actions>> GetAllActions(); 
        Task<bool> Update(int id, Actions actions); 

        Task<bool> Create(Actions actions);

        Task<Actions> FetchSingleResult(int id);  

        Task<bool> Delete(int id);   

        Task<bool> CheckUniqueName(int id, string actions);  

        Task<PagedResult<Actions>> GetPagedActions(ActionsSearchDto model);
    }
}
