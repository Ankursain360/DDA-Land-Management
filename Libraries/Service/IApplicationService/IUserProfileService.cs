using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
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
        Task<bool> ValidateUniqueRoleName(int id, string name);
        Task<bool> ValidateUniqueUserName(int id, string UserName);
        Task<bool> UpdateUserPersonalDetails(UserPersonalInfoDto model);
        Task<bool> UpdateUserProfileDetails(UserProfileEditDto model);
        Task<bool> DeleteRole(RoleDto model);
        Task<bool> DeleteUser(int id);
        Task<List<ZoneDto>> GetAllZone(int departmentId);
    }
}
