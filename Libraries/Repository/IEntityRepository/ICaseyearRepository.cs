using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ICaseyearRepository : IGenericRepository<Caseyear>
    {
        Task<PagedResult<Caseyear>> GetPagedCaseyear(CaseyearSearchDto model);

        Task<List<Caseyear>> GetAllCaseyear();

    }
}
