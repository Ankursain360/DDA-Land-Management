using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IZoneService : IEntityService<Zone>
    {
        Task<List<Zone>> GetAllZone(); // To Get all data added by renu
       
        Task<bool> Update(int id, Zone zone); // To Upadte Particular data added by renu

        Task<bool> Create(Zone zone);

        Task<Zone> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string zone);// To check Unique Value  for zone
        Task<bool> CheckUniqueCode(int id, string code);// To check Unique Value  for zone
        Task<List<Department>> GetDropDownList();
        Task<List<Zone>> GetAllDetails();
    }
}
