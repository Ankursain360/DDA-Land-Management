using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ICaseyearService : IEntityService<Caseyear>
    {

        Task<List<Caseyear>> GetAllCaseyear();
        Task<bool> Create(Caseyear caseyear);

        Task<PagedResult<Caseyear>> GetPagedCaseyear(CaseyearSearchDto model);

        Task<bool> Update(int id, Caseyear caseyear);
        Task<Caseyear> FetchSingleResult(int id);

    }
}
