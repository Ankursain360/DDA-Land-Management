using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMonthlyRosterRepository : IGenericRepository<MonthlyRoaster>
    {
        Task<List<Department>> GetAllDepartmentList();
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Division>> GetAllDivisionList(int zoneId);
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId, int departmentId, int zoneId, int localityId);
        Task<List<Userprofile>> SecurityGuardList();
    }
}
