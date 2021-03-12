using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILogRepository : IGenericRepository<Log>
    {

        Task<List<Log>> GetLog();
      
        Task<PagedResult<Log>> GetPagedLog(LogSearchDto model);
      
       
    }
}
