using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IZoneService : IEntityService<Zone>
    {
        Task<bool> Update(int id, Zone zone); // To Upadte Particular data added by renu

        Task<bool> Create(Zone zone);

        Task<Zone> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string zone);// To check Unique Value  for zone
        Task<bool> CheckUniqueCode(int id, string code);// To check Unique Value  for zone
        Task<List<Department>> GetDropDownList();
        Task<List<Zone>> GetAllDetails();
        Task<PagedResult<Zone>> GetPagedZone(ZoneSearchDto model);
    }
}
