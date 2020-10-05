using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface ISchemeRepository : IGenericRepository<Scheme>
    {

        Task<List<Scheme>> GetAllScheme();
        Task<PagedResult<Scheme>> GetPagedScheme(SchemeSearchDto model);

        Task<bool> Any(int id, string name);
    }
}

