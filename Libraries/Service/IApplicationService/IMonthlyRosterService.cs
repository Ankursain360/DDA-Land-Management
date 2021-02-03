using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IMonthlyRosterService : IEntityService<MonthlyRoaster>
    {
        Task<List<Department>> GetAllDepartmentList();
        Task<bool> Create(MonthlyRoaster monthlyRoaster);
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Division>> GetAllDivisionList(int zoneId);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Userprofile>> SecurityGuardList();
        Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId, int departmentId, int zoneId, int localityId);
        Task<PagedResult<MonthlyRoaster>> GetAllRoasterDetails(MonthlyRoasterSearchDto monthlyRoasterSearchDto);
        Task<MonthlyRoaster> GetMonthlyRoasterById(int id);
        Task<bool> Update(int id, MonthlyRoaster monthlyRoaster);
        Task<bool> DeleteRoaster(int id);
    }
}
