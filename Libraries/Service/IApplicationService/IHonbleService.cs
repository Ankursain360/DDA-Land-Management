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

    public interface IHonbleService : IEntityService<Honble>
    {
        Task<List<Honble>> GetAllHonble();

        Task<bool> Update(int id, Honble rent);
        Task<bool> Create(Honble rate);
        Task<Honble> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Honble>> GetPagedHonble(HonbleSearchDto model);

    }
}
