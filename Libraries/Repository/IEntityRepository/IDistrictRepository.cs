using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
   public interface IDistrictRepository:IGenericRepository<District>
    {
        Task<List<District>> GetDistricts();
        Task<PagedResult<District>> GetPagedDistrict(DistrictSearchDto model);
    }
}
