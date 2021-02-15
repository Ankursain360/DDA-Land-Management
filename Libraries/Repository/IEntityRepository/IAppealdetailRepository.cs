using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAppealdetailRepository : IGenericRepository<Appealdetail>
    {
        Task<List<Appealdetail>> GetAppealdetail();
       
        Task<PagedResult<Appealdetail>> GetPagedAppealdetail(AppealdetailSearchDto model);
    }
}

