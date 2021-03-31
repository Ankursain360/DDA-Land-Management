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

    public interface IJudgementstatusService : IEntityService<Judgementstatus>
    {
        Task<List<Judgementstatus>> GetAllJudgementstatus();

        Task<bool> Update(int id, Judgementstatus rent);
        Task<bool> Create(Judgementstatus rate);
        Task<Judgementstatus> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Judgementstatus>> GetPagedJudgementstatus(JudgementstatusSearchDto model);

    }
}
