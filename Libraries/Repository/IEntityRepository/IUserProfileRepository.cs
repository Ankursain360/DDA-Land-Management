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
        Task<List<Userprofile>> GetAllUser();
        Task<List<Userprofile>> GetUser();
        Task<List<ApplicationRole>> GetRole();
        Task<Userprofile> GetUserById(int userId);

       
        Task<List<KycApplicationSearchDto>> KycApplicationDetails(int id);

        Task<List<KycDemandPaymentSearchDto>> KycDemandPaymentDetails(int id);
        Task<List<ApplicationRole>> GetActiveRole();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<bool> ValidateUniqueRoleName(int id, string name);
        Task<bool> ValidateUniqueUserName(int id, string userName);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<List<Userprofile>> GetUserOnRoleBasis(int roleId);
        Task<List<UserProfileInfoDetailsDto>> GetUserSkippingItsOwnConcatedName(int roleId, int userid);
        Task<List<UserWithRoleDto>> GetUserWithRole();
        Task<List<Userprofile>> GetUserOnRoleZoneBasis(int roleId, int zoneId);
        Task<List<Userprofile>> GetUserByIdZone(int userid, int zoneId);
        Task<List<Userprofile>> UserListSkippingmultiusers(int[] nums);
        Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleZoneBasisConcatedName(int roleId, int zoneId);

        Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleBasisConcatedName(int roleId);
        Task<List<UserProfileInfoDetailsDto>> GetUserByIdZoneConcatedName(int userid, int zoneId);
        Task<List<UserProfileInfoDetailsDto>> UserListSkippingmultiusersConcatedName(int[] nums);
        Task<bool> ValidateUniqueEmail(int id, string email);
        Task<bool> ValidateUniquePhone(int id, string phonenumber);
        Task<bool> ValidateUniqueEmail1(string email);
        Task<bool> ValidateUniquePhone1(string phonenumber);
        Task<bool> ValidateUniqueUserName1(string userName);

        //added by ishu 20/7/2021
        Task<List<Userprofile>> GetUserOnRoleBranchBasis(int roleId, int branchId);
        Task<List<Userprofile>> GetUserByIdBranch(int userid, int branchId);
        Task<List<kycUserProfileInfoDetailsDto>> kycUserListSkippingmultiusersConcatedName(int[] nums);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> GetUserByIdBranchConcatedName(int userid, int branchId);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> GetkycUserOnRoleBasisConcatedName(int roleId);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> kycGetUserOnRoleZoneBasisConcatedName(int roleId, int branchId);//added by ishu 23/7/2021
        Task<List<kycUserProfileInfoDetailsDto>> GetkycUserSkippingItsOwnConcatedName(int roleId, int userid);//added by ishu 23/7/2021
    }
}
