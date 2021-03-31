using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IJudgementstatusRepository : IGenericRepository<Judgementstatus>
    {
        Task<PagedResult<Judgementstatus>> GetPagedJudgementstatus(JudgementstatusSearchDto model);
        Task<List<Judgementstatus>> GetAllJudgementstatus();
    }
}
