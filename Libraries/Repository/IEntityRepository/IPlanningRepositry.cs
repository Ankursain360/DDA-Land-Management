using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPlanningRepositry : IGenericRepository<Planning>
    {
        Task<PagedResult<Planning>> GetPagedPlanning(PlanningSearchDto dto);
        Task<PagedResult<Planning>> GetUnverifiedPagedPlanning(PlanningSearchDto dto);
        Task<List<Division>> GetAllDivision(int ZoneId);
        Task<List<Zone>> GetAllZone(int DepartmentId);
        Task<List<Department>> GetAllDepartment();
        Task<bool> DeleteProperties(int planningId);
        Task<List<Propertyregistration>> GetPlannedProperties(int departmentId, int zoneId, int divisionId);
        Task<List<Propertyregistration>> GetUnplannedProperties(int departmentId, int zoneId, int divisionId);
        Task<bool> CreateProperties(List<PlanningProperties> planningProperties);
        Task<List<int>> FetchUnplannedProperties(int id);
        Task<List<int>> FetchPlannedProperties(int id);
    }
}
