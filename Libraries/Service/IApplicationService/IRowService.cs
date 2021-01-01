using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IRowService : IEntityService<Row>
    {
        Task<List<Row>> GetAllRow();
        Task<List<Row>> GetRowUsingReport();

        Task<bool> Update(int id, Row row);
        Task<bool> Create(Row row);
        Task<Row> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string row);

        Task<PagedResult<Row>> GetPagedRow(RowSearchDto model);
    }
}
