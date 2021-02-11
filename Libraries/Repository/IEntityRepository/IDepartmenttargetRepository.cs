using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
   public interface IDepartmenttargetRepository: IGenericRepository<Departmenttarget>
    {
        Task<PagedResult<Departmenttarget>> GetPagedDepartmenttarget(DepartmentTargetSearchDto model);
        Task<List<Departmenttarget>> GetDepartmenttarget();
        Task<bool> Any(int id, string name);
        Task<List<Departmenttarget>> GetAllDepartmenttarget();
        Task<bool> AnyName(int Id, string Name, int DepartmentId);
        Task<bool> AnyCode(int id, int code);
        Task<List<Department>> GetAllDepartment();
        Task<List<WeeklyFileReportListDataDto>> GetPagedWeeklyFileReport(WeeklyFileReportSearchDto model, int UserId);

    }
}
