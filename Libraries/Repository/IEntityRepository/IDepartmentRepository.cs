using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<PagedResult<Department>> GetPagedDepartment(DepartmentSearchDto model);
        Task<List<Department>> GetDepartment();
        Task<bool> Any(int id, string name);
    }
}