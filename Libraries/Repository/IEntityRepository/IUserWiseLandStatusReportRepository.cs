using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository

{
    public interface IUserWiseLandStatusReportRepository : IGenericRepository<Vacantlandimage>
    {


        Task<List<Zone>> GetAllZone(int departmentId);


        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();

        Task<PagedResult<Vacantlandimage>> GetPagedUserWiseLandStatusReport(UserWiseLandStatusReportSearchDto model);
    }
}
