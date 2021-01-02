using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IColumnRepository : IGenericRepository<Column>
    {
        Task<List<Column>> GetColumn();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Column>> GetPagedColumn(ColumnSearchDto model);


    }
}
