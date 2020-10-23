using Dto.Master;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IUserProfileService : IEntityService<Userprofile>
    {
        Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model);
        Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model);
        Task<RoleDto> GetRoleById(int id);
        Task<bool> UpdateRole(RoleDto model);
        Task<List<UserProfileDto>> GetUser();
        Task<UserProfileDto> GetUserById(int userId);
        Task<List<RoleDto>> GetRole();
        Task<List<RoleDto>> GetActiveRole();
        Task<bool> CreateUser(AddUserDto userDto);
        Task<bool> UpdateUser(EditUserDto userDto);
    }
}
