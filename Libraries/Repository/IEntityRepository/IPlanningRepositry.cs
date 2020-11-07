using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPlanningRepositry: IGenericRepository<Planning>
    {
        Task<PagedResult<Planning>> GetPagedPlanning();
        Task<List<Division>> GetAllDivision(int ZoneId);
        Task<List<Zone>> GetAllZone(int DepartmentId);
        Task<List<Department>> GetAllDepartment();
    }
}
