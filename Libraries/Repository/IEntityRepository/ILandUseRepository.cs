using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface ILandUseRepository : IGenericRepository<Landuse>
    {

        Task<bool> Any(int id, string name);
        Task<PagedResult<Landuse>> GetPagedLandUse(LandUseSearchDto model);
    }
}