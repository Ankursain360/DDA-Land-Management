using Dto.Search;
using Libraries.Repository.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IEntityRepository
{
    public interface IUserProfileRepository : IGenericRepository<Userprofile>
    {
        Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model);
        Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model);
        Task<List<Userprofile>> GetUser();
        Task<List<ApplicationRole>> GetRole();
        Task<Userprofile> GetUserById(int userId);
        Task<List<ApplicationRole>> GetActiveRole();
    }
}
