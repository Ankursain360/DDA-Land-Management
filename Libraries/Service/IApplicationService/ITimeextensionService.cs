
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

    public interface ITimeextensionService : IEntityService<Timeextension>
    {

        Task<List<Timeextension>> GetAllTimeextension();
        Task<bool> Update(int id, Timeextension time);
        Task<bool> Create(Timeextension time);
        Task<Timeextension> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Timeextension>> GetPagedTimeextension(TimeextensionSearchDto model);

    }
}
