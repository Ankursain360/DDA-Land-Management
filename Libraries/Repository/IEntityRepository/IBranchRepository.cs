using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {

        Task<bool> Any(int id, string name);
        Task<bool> anyCode(int id, string name);
        Task<List<Department>> GetDepartmentList();
        Task<List<Branch>> GetAllDetails();
        Task<PagedResult<Branch>> GetPagedBranch(BranchSearchDto model);
    }
}
