using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IDepartmenttargetService : IEntityService<Departmenttarget>
    {
        Task<List<DepartmenttargetDto>> GetDepartmenttarget();
        Task<PagedResult<Departmenttarget>> GetPagedDepartmenttarget(DepartmentTargetSearchDto model);
        Task<List<Departmenttarget>> GetAllDepartmenttarget(); // To Get all data added
        Task<List<Departmenttarget>> GetDepartmenttargetUsingRepo();

        Task<bool> Update(int id, Departmenttarget departmenttarget); // To Upadte Particular data added 

        Task<bool> Create(Departmenttarget departmenttarget);

        Task<Departmenttarget> FetchSingleResult(int id);  // To fetch Particular data added 

        Task<bool> Delete(int id);    // To delete

        Task<bool> CheckUniqueName(int id, string department);   // To check Unique Value  
        Task<bool> CheckUniqueCode(int id, int code);
        Task<List<Department>> GetAllDepartment();
        Task<List<WeeklyFileReportListDataDto>> GetPagedWeeklyFileReport(WeeklyFileReportSearchDto model, int UserId);
    }

}
