using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IPlanningService: IEntityService<Planning>
    {
        Task<bool> Update(int id, Planning planning);
        Task<bool> Create(Planning planning);
        Task<PagedResult<Planning>> GetPagedPlanning();
        Task<List<Division>> GetAllDivision(int ZoneId);
        Task<List<Zone>> GetAllZone(int DepartmentId);
        Task<List<Department>> GetAllDepartment();
        Task<Planning> FetchSingleResult(int id);
        Task<bool> Delete(int id);
    }
}
