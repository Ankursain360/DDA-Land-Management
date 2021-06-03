using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IVillageService : IEntityService<Village>
    {
        Task<List<Village>> GetAllVillage(); // To Get all data added by Praveen
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data added by Praveen
        Task<List<Village>> GetVillageUsingRepo();
        Task<bool> Update(int id, Village village); // To Upadte Particular data added by Praveen
        Task<bool> Create(Village Village);
        Task<Village> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
        Task<bool> CheckUniqueName(int id, string name);   // To check Unique Value  for Village
        Task<PagedResult<Village>> GetPagedVillage(VillageSearchDto model);
        Task<List<Division>> GetAllDivisionList(int zoneId); // To Get all data added by Praveen
        Task<List<Department>> GetAllDepartment();
        Task<List<Zone>> GetAllZone();
    }
}
