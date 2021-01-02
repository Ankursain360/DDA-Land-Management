using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IRowRepository : IGenericRepository<Row>
    {
        Task<List<Row>> GetRow();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Row>> GetPagedRow(RowSearchDto model);


    }
    
}
