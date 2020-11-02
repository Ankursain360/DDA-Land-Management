using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ICurrentstatusoflandhistoryService : IEntityService<Currentstatusoflandhistory>
    {
        Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int id);
        Task<bool> Create(Currentstatusoflandhistory model);

        Task<Currentstatusoflandhistory> FetchSingleResult(int id);
        Task<PagedResult<Currentstatusoflandhistory>> GetPagedCurrentstatusoflandhistory(CurrentstatusoflandhistorySearchDto model);

    }
}
