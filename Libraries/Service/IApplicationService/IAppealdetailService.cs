using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IAppealdetailService : IEntityService<Appealdetail>
    {
        Task<List<Appealdetail>> GetAllAppealdetail();
        Task<List<Appealdetail>> GetAppealdetailUsingRepo();

        Task<bool> Update(int id, Appealdetail appealdetail);
        Task<bool> Create(Appealdetail appealdetail);
        Task<Appealdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        

        Task<PagedResult<Appealdetail>> GetPagedAppealdetail(AppealdetailSearchDto model);

     

    }
}

