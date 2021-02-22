using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandSchemeRepository : IGenericRepository<Newlandscheme>
    {

        Task<List<Newlandscheme>> GetAllScheme();
        Task<PagedResult<Newlandscheme>> GetPagedScheme(NewlandschemeSearchDto model);

        Task<bool> Any(int id, string name);

    }
}
