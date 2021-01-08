using Libraries.Model.Entity;
using Libraries.Service.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IMonthlyRosterService : IEntityService<MonthlyRoaster>
    {
        Task<List<Department>> GetAllDepartmentList();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Division>> GetAllDivisionList(int zoneId);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Userprofile>> SecurityGuardList();
        Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId, int departmentId, int zoneId, int localityId);
    }
}
