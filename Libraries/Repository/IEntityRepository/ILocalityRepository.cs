using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILocalityRepository : IGenericRepository<Locality>
    {
        Task<List<Locality>> GetAllLocality();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Department>> GetAllDepartment();
        Task<bool> AnyName(int id, string name);
        Task<bool> AnyCode(int id, string name);
        Task<PagedResult<Locality>> GetPagedLocality(LocalitySearchDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
    }
}
