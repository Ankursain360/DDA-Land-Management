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
        Task<List<Userprofile>> GetAllUser();
        Task<RoleDto> GetRoleById(int id);
        Task<bool> UpdateRole(RoleDto model);
        Task<List<UserProfileDto>> GetUser();
        Task<UserProfileDto> GetUserById(int userId);
        Task<List<KycApplicationSearchDto>> KycApplicationDetails(int userId);

        Task<List<KycDemandPaymentSearchDto>> KycDemandPaymentDetails(int userId);
        Task<List<RoleDto>> GetRole();
        Task<List<RoleDto>> GetActiveRole();
        Task<bool> CreateUser(AddUserDto userDto);
        Task<bool> UpdateUser(EditUserDto userDto);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<bool> ValidateUniqueRoleName(int id, string name);
        Task<bool> ValidateUniqueUserName(int id, string UserName);
        Task<bool> UpdateUserPersonalDetails(UserPersonalInfoDto model);
        Task<bool> UpdateUserProfileDetails(UserProfileEditDto model);
        Task<bool> DeleteRole(RoleDto model);
        Task<bool> DeleteUser(int id);
        Task<List<ZoneDto>> GetAllZone(int departmentId);
        Task<List<UserProfileDto>> GetUserOnRoleBasis(int roleId);
        Task<List<UserProfileInfoDetailsDto>> GetUserSkippingItsOwnConcatedName(int roleId, int userid);
        Task<List<UserWithRoleDto>> GetUserWithRole();
        Task<List<UserProfileDto>> GetUserOnRoleZoneBasis(int roleId, int zoneId);
        Task<UserProfileDto> GetUserByIdZone(int userid, int zoneId);
        Task<List<UserProfileDto>> UserListSkippingmultiusers(int[] nums);
        Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleZoneBasisConcatedName(int roleId, int zoneId);
        Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleBasisConcatedName(int roleId);
        Task<UserProfileInfoDetailsDto> GetUserByIdZoneConcatedName(int userid, int zoneId);
        Task<List<UserProfileInfoDetailsDto>> UserListSkippingmultiusersConcatedName(int[] nums);
        Task<bool> ValidateUniqueEmail(int id, string email);
        Task<bool> ValidateUniquePhone(int id, string phonenumber);
        Task<bool> ValidateUniqueEmail1(string email);
        Task<bool> ValidateUniquePhone1(string phonenumber);
        Task<bool> ValidateUniqueUserName1(string Name);

        //added by ishu 20/7/2021
        Task<List<UserProfileDto>> GetUserOnRoleBranchBasis(int roleId, int branchId);
        Task<UserProfileDto> GetUserByIdBranch(int userid, int branchId);
        Task<List<kycUserProfileInfoDetailsDto>> kycUserListSkippingmultiusersConcatedName(int[] nums);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> kycGetUserOnRoleZoneBasisConcatedName(int roleId, int branchId);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> GetkycUserOnRoleBasisConcatedName(int roleId);//added by ishu 23/7/2021
       // Task<List<kycUserProfileInfoDetailsDto>> GetUserByIdBranchConcatedName(int userid, int branchId);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> GetkycUserSkippingItsOwnConcatedName(int roleId, int userid);//added by ishu 23/7/2021
        Task<kycUserProfileInfoDetailsDto> GetUserByIdBranchConcatedName(int userid, int branchId);//added by ishu 23/7/2021
    }
}
