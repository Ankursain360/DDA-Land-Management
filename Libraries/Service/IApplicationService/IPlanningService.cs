using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IPlanningService : IEntityService<Planning>
    {
        Task<bool> Update(int id, Planning planning);
        Task<bool> VerifyProperty(int id, Planning planning);
        Task<bool> Create(Planning planning);
        Task<PagedResult<Planning>> GetPagedPlanning(PlanningSearchDto dto);
        Task<PagedResult<Planning>> GetUnverifiedPagedPlanning(PlanningSearchDto dto);
        Task<List<Division>> GetAllDivision(int ZoneId);
        Task<List<Zone>> GetAllZone(int DepartmentId);
        Task<List<Department>> GetAllDepartment();
        Task<Planning> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Propertyregistration>> GetPlannedProperties(int departmentId, int zoneId, int divisionId);
        Task<List<Propertyregistration>> GetUnplannedProperties(int departmentId, int zoneId, int divisionId);
        Task<bool> CreateProperties(List<PlanningProperties> planningProperties);
        Task<bool> VerifyProperties(int plannigId);
        Task<List<int>> FetchUnplannedProperties(int id);
        Task<List<int>> FetchPlannedProperties(int id);
    }
}
