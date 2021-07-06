using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.IApplicationService
{
    public interface IUserWiseLandStatusReportService : IEntityService<Vacantlandimage>
    {

        Task<List<Zone>> GetAllZone(int departmentId); 
        Task<List<Department>> GetAllDepartment();

        Task<List<Division>> GetAllDivisionList(int zone);

        Task<PagedResult<Vacantlandimage>> GetPagedUserWiseLandStatusReport(UserWiseLandStatusReportSearchDto model);
    }
}
