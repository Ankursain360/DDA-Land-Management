using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
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
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<bool> ValidateUniqueRoleName(int id, string name);
        Task<bool> ValidateUniqueUserName(int id, string userName);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<List<Userprofile>> GetUserOnRoleBasis(int roleId);
        Task<List<Userprofile>> GetUserSkippingItsOwn(int roleId, int userid);
        Task<List<UserWithRoleDto>> GetUserWithRole();
        Task<List<Userprofile>> GetUserOnRoleZoneBasis(int roleId, int zoneId);
        Task<List<Userprofile>> GetUserByIdZone(int userid, int zoneId);
        Task<List<Userprofile>> UserListSkippingmultiusers(int[] nums);
    }
}
