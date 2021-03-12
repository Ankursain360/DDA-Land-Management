using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ILogService : IEntityService<Log>
    {
        Task<List<Log>> GetLog();
        
       
        Task<List<Log>> GetLogUsingRepo();
        //Task<bool> Update(int id, Log log);
        //Task<bool> Create(Acquiredlandvillage acquiredlandvillage);
        Task<Log> FetchSingleResult(int id);
        //Task<bool> Delete(int id);
      

        Task<PagedResult<Log>> GetPagedLog(LogSearchDto model);
       


    }
}
