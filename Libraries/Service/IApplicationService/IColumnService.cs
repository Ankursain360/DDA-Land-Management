using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IColumnService : IEntityService<Column>
    {
        Task<List<Column>> GetAllColumn();
        Task<List<Column>> GetColumnUsingReport();

        Task<bool> Update(int id, Column column);
        Task<bool> Create(Column column);
        Task<Column> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string column);

        Task<PagedResult<Column>> GetPagedColumn(ColumnSearchDto model);
    }
}
