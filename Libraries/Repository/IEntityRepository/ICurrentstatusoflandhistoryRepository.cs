using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface ICurrentstatusoflandhistoryRepository : IGenericRepository<Currentstatusoflandhistory>
    {

        Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int id);
        Task<Currentstatusoflandhistory> FetchSingleResult(int id);
        Task<PagedResult<Currentstatusoflandhistory>> GetPagedCurrentstatusoflandhistory(CurrentstatusoflandhistorySearchDto model);
    } 
}
