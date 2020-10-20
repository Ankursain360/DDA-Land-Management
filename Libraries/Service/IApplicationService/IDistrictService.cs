using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IDistrictService : IEntityService<District>
    {
        Task<List<District>> GetAllDistrict();
        Task<List<District>> GetDistrictUsingRepo();

        Task<bool> Update(int id, District district);
        Task<bool> Create(District district);
        Task<District> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string district);

        Task<PagedResult<District>> GetPagedDistrict(DistrictSearchDto model);

        //Task<bool> Create();

    }
}
