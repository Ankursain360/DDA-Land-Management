using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.Common;
using Repository.IEntityRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore.Internal;
using System;



namespace Repository.EntityRepository
{
    public class UserProfileRepository : GenericRepository<Userprofile>, IUserProfileRepository
    {
        public UserProfileRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Userprofile>> GetAllUser()
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .ToListAsync();
        }
        public async Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model)
        {

            var data = await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Include(a => a.Branch)
                                    .Where(a => (string.IsNullOrEmpty(model.UserName) || a.User.UserName.Contains(model.UserName))
                                        && (string.IsNullOrEmpty(model.Name) || a.User.Name.Contains(model.Name))
                                        && (string.IsNullOrEmpty(model.Email) || a.User.Email.Contains(model.Email))
                                        && (string.IsNullOrEmpty(model.RoleName) || a.Role.Name.Contains(model.RoleName))
                                        && (string.IsNullOrEmpty(model.ZoneName) || a.Zone.Name.Contains(model.ZoneName))
                                        && (string.IsNullOrEmpty(model.PhoneNumber) || a.User.PhoneNumber.Contains(model.PhoneNumber))
                                        && (a.IsActive == 1)
                                        )
                                    .GetPaged<Userprofile>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("USERNAME"):
                        data.Results = data.Results.OrderBy(x => x.User.UserName).ToList();

                        break;
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.User.Name).ToList();
                        break;

                    case ("ROLE"):
                        data.Results = data.Results.OrderBy(x => x.Role.Name).ToList();
                        break;

                    case ("PHONENUMBER"):
                        data.Results = data.Results.OrderBy(x => x.User.PhoneNumber).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderBy(x => x.User.Email).ToList();
                        break;

                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("USERNAME"):
                        data.Results = data.Results.OrderByDescending(x => x.User.UserName).ToList();

                        break;
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.User.Name).ToList();
                        break;

                    case ("ROLE"):
                        data.Results = data.Results.OrderByDescending(x => x.Role.Name).ToList();
                        break;

                    case ("PHONENUMBER"):
                        data.Results = data.Results.OrderByDescending(x => x.User.PhoneNumber).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderByDescending(x => x.User.Email).ToList();
                        break;

                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }

            return data;
        }

        public async Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model)
        {
            var data = await _dbContext.Roles
                           .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Roles
                           .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .OrderBy(x => x.Name)
                           .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Roles
                           .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Roles
                           .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .OrderByDescending(x => x.Name)
                           .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Roles
                           .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<List<Userprofile>> GetUser()
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => a.IsActive == 1)
                                    .ToListAsync();
        }

        public async Task<Userprofile> GetUserById(int userId)
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => a.IsActive == 1 && a.User.Id == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Userprofile> GetUserProfileById(int userId)
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => a.User.Id == userId).OrderByDescending(a => a.Id)
                                    .FirstOrDefaultAsync();
        }


        public async Task<List<Jaraiowner>> GetAllOwner(int id)
        {
            return await _dbContext.Jaraiowner.Where(x => x.JaraiDetailId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<KycApplicationSearchDto>> KycApplicationDetails(int userId)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("GetKycApplicationDetailsForChart")
                                            .WithSqlParams(("User_Id", userId))
                                            .ExecuteStoredProcedureAsync<KycApplicationSearchDto>();

                return (List<KycApplicationSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<KycDemandPaymentSearchDto>> KycDemandPaymentDetails(int userId)
        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("GetKycDemandPaymentDetailsForChart")
                                            .WithSqlParams(("User_Id", userId))
                                            .ExecuteStoredProcedureAsync<KycDemandPaymentSearchDto>();

                return (List<KycDemandPaymentSearchDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ApplicationRole>> GetRole()
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<List<ApplicationRole>> GetActiveRole()
        {
            return await _dbContext.Roles.Where(a => a.IsActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<bool> ValidateUniqueRoleName(int id, string name)
        {
            return await _dbContext.Roles.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> ValidateUniqueUserName(int id, string userName)
        {
            return await _dbContext.Users.AnyAsync(t => t.Id != id && t.UserName.ToLower() == userName.ToLower());
        }
        public async Task<bool> ValidateUniqueUserName1(string userName)
        {
            return await _dbContext.Users.AnyAsync(t => t.UserName.ToLower() == userName.ToLower() || t.NormalizedUserName == userName.ToUpper());
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
            return zoneList;
        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _dbContext.Possesionplan
                                    .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Include(x => x.Allotment.LeasePurposesType)
                                    .Include(x => x.Allotment.LeasesType)
                                    .Where(x => x.Allotment.Application.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Userprofile>> GetUserOnRoleBasis(int roleId)
        {
            return await _dbContext.Userprofile
                                   .Include(a => a.User)
                                   .Include(a => a.Role)
                                   .Include(a => a.Department)
                                   .Include(a => a.Zone)
                                   .Include(a => a.District)
                                   .Where(a => a.IsActive == 1 && a.RoleId == roleId)
                                   .ToListAsync();
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserSkippingItsOwnConcatedName(int roleId, int userid)
        {
            List<UserProfileInfoDetailsDto> listData = new List<UserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                   .Include(a => a.User)
                                   .Include(a => a.Role)
                                   .Include(a => a.Department)
                                   .Include(a => a.Zone)
                                   .Include(a => a.District)
                                   .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.User.Id != userid)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new UserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        ZoneId = Data[i].ZoneId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        ZoneName = Data[i].Zone == null ? "" : Data[i].Zone.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Zone == null ? "NA" : Data[i].Zone.Name.ToUpper())
                    });
                }
            }

            return listData;
        }



        public async Task<List<kycUserProfileInfoDetailsDto>> GetkycUserSkippingItsOwnConcatedName(int roleId, int userid)//added by ishu 23/7/2021
        {
            List<kycUserProfileInfoDetailsDto> listData = new List<kycUserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                   .Include(a => a.User)
                                   .Include(a => a.Role)
                                   .Include(a => a.Department)
                                   .Include(a => a.Branch)
                                   .Include(a => a.District)
                                   .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.User.Id != userid)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new kycUserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        BranchId = Data[i].BranchId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        BranchName = Data[i].Zone == null ? "" : Data[i].Branch.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Branch == null ? "NA" : Data[i].Branch.Name.ToUpper())
                    });
                }
            }

            return listData;
        }

        public async Task<List<UserWithRoleDto>> GetUserWithRole()
        {
            var data = await _dbContext.LoadStoredProcedure("UserWithRoleDropdownBind")
                                        .WithOutParams()
                                        .ExecuteStoredProcedureAsync<UserWithRoleDto>();

            return (List<UserWithRoleDto>)data;
        }

        public async Task<List<Userprofile>> GetUserOnRoleZoneBasis(int roleId, int zoneId)
        {
            return await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Zone)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.ZoneId == zoneId)
                                  .ToListAsync();
        }
        public async Task<List<Userprofile>> GetUserOnRoleBranchBasis(int roleId, int branchId)
        {
            return await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Zone)
                                  .Include(a => a.District)
                                  .Include(a => a.Branch)
                                  .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.BranchId == branchId)
                                  .ToListAsync();
        }

        public async Task<List<Userprofile>> GetUserByIdZone(int userid, int zoneId)
        {
            return await _dbContext.Userprofile
                                .Include(a => a.User)
                                .Include(a => a.Role)
                                .Include(a => a.Department)
                                .Include(a => a.Zone)
                                .Include(a => a.District)
                                .Where(a => a.IsActive == 1 && a.UserId == userid && a.ZoneId == zoneId)
                                .ToListAsync();
        }
        public async Task<List<Userprofile>> GetUserByIdBranch(int userid, int branchId)
        {
            return await _dbContext.Userprofile
                                .Include(a => a.User)
                                .Include(a => a.Role)
                                .Include(a => a.Department)
                                .Include(a => a.Zone)
                                .Include(a => a.District)
                                .Include(a => a.Branch)
                                .Where(a => a.IsActive == 1 && a.UserId == userid && a.BranchId == branchId)
                                .ToListAsync();
        }

        public async Task<List<Userprofile>> UserListSkippingmultiusers(int[] nums)
        {
            return await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Zone)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && nums.Contains(a.UserId))
                                  .ToListAsync();
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleZoneBasisConcatedName(int roleId, int zoneId)
        {
            List<UserProfileInfoDetailsDto> listData = new List<UserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Zone)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.ZoneId == zoneId)
                                  .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new UserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        ZoneId = Data[i].ZoneId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        ZoneName = Data[i].Zone == null ? "" : Data[i].Zone.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Zone == null ? "NA" : Data[i].Zone.Name.ToUpper())
                    });
                }
            }

            return listData;
        }

        public async Task<List<kycUserProfileInfoDetailsDto>> kycGetUserOnRoleZoneBasisConcatedName(int roleId, int branchId)//added by ishu 23/7/2021
        {
            List<kycUserProfileInfoDetailsDto> listData = new List<kycUserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Branch)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && a.RoleId == roleId && a.BranchId == branchId)
                                  .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new kycUserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        BranchId = Data[i].BranchId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        BranchName = Data[i].Branch == null ? "" : Data[i].Branch.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Branch == null ? "NA" : Data[i].Branch.Name.ToUpper())
                    });
                }
            }

            return listData;
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleBasisConcatedName(int roleId)
        {
            List<UserProfileInfoDetailsDto> listData = new List<UserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                   .Include(a => a.User)
                                   .Include(a => a.Role)
                                   .Include(a => a.Department)
                                   .Include(a => a.Zone)
                                   .Include(a => a.District)
                                   .Where(a => a.IsActive == 1 && a.RoleId == roleId)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new UserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        ZoneId = Data[i].ZoneId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        ZoneName = Data[i].Zone == null ? "" : Data[i].Zone.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Zone == null ? "NA" : Data[i].Zone.Name.ToUpper())
                    });
                }
            }

            return listData;
        }


        public async Task<List<kycUserProfileInfoDetailsDto>> GetkycUserOnRoleBasisConcatedName(int roleId)//added by ishu 23/7/2021
        {
            List<kycUserProfileInfoDetailsDto> listData = new List<kycUserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                   .Include(a => a.User)
                                   .Include(a => a.Role)
                                   .Include(a => a.Department)
                                   .Include(a => a.Branch)
                                   .Include(a => a.District)
                                   .Where(a => a.IsActive == 1 && a.RoleId == roleId)
                                   .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new kycUserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        BranchId = Data[i].BranchId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        BranchName = Data[i].Branch == null ? "" : Data[i].Branch.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Branch == null ? "NA" : Data[i].Branch.Name.ToUpper())
                    });
                }
            }

            return listData;
        }


        public async Task<List<UserProfileInfoDetailsDto>> GetUserByIdZoneConcatedName(int userid, int zoneId)
        {
            List<UserProfileInfoDetailsDto> listData = new List<UserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                .Include(a => a.User)
                                .Include(a => a.Role)
                                .Include(a => a.Department)
                                .Include(a => a.Zone)
                                .Include(a => a.District)
                                .Where(a => a.IsActive == 1 && a.UserId == userid && a.ZoneId == zoneId)
                                .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new UserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        ZoneId = Data[i].ZoneId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        ZoneName = Data[i].Zone == null ? "" : Data[i].Zone.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Zone == null ? "NA" : Data[i].Zone.Name.ToUpper())
                    });
                }
            }

            return listData;
        }

        public async Task<List<kycUserProfileInfoDetailsDto>> GetUserByIdBranchConcatedName(int userid, int branchId)//added by ishu 23/7/2021
        {
            List<kycUserProfileInfoDetailsDto> listData = new List<kycUserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                .Include(a => a.User)
                                .Include(a => a.Role)
                                .Include(a => a.Department)
                                .Include(a => a.Branch)
                                .Include(a => a.District)
                                .Where(a => a.IsActive == 1 && a.UserId == userid && a.BranchId == branchId)
                                .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new kycUserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        BranchId = Data[i].BranchId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        BranchName = Data[i].Branch == null ? "" : Data[i].Branch.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Branch == null ? "NA" : Data[i].Branch.Name.ToUpper())
                    });
                }
            }

            return listData;
        }


        public async Task<List<UserProfileInfoDetailsDto>> UserListSkippingmultiusersConcatedName(int[] nums)
        {
            List<UserProfileInfoDetailsDto> listData = new List<UserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Zone)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && nums.Contains(a.UserId))
                                  .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new UserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        ZoneId = Data[i].ZoneId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        ZoneName = Data[i].Zone == null ? "" : Data[i].Zone.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Zone == null ? "NA" : Data[i].Zone.Name.ToUpper())
                    });
                }
            }

            return listData;
        }


        public async Task<List<kycUserProfileInfoDetailsDto>> kycUserListSkippingmultiusersConcatedName(int[] nums)//added by ishu 23/7/2021
        {
            List<kycUserProfileInfoDetailsDto> listData = new List<kycUserProfileInfoDetailsDto>();

            var Data = await _dbContext.Userprofile
                                  .Include(a => a.User)
                                  .Include(a => a.Role)
                                  .Include(a => a.Department)
                                  .Include(a => a.Branch)
                                  .Include(a => a.District)
                                  .Where(a => a.IsActive == 1 && nums.Contains(a.UserId))
                                  .ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new kycUserProfileInfoDetailsDto()
                    {
                        Id = Data[i].Id,
                        UserId = Data[i].UserId,
                        BranchId = Data[i].BranchId ?? 0,
                        RoleId = Data[i].RoleId ?? 0,
                        UserName = Data[i].User == null ? "" : Data[i].User.Name,
                        RoleName = Data[i].Role == null ? "" : Data[i].Role.Name,
                        BranchName = Data[i].Branch == null ? "" : Data[i].Branch.Name,
                        Name = string.Format("{0}|{1}|{2}", Data[i].User == null ? "NA" : Data[i].User.Name.ToUpper(), Data[i].Role == null ? "NA" : Data[i].Role.Name.ToUpper(), Data[i].Branch == null ? "NA" : Data[i].Branch.Name.ToUpper())
                    });
                }
            }

            return listData;
        }

        public async Task<bool> ValidateUniqueEmail(int id, string email)
        {
            return await _dbContext.Users.AnyAsync(t => t.Id != id && t.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> ValidateUniqueEmail1(string email)
        {
            return await _dbContext.Users.AnyAsync(t => t.Email.ToLower() == email.ToLower() || t.NormalizedEmail == email.ToUpper());
        }

        public async Task<bool> ValidateUniquePhone(int id, string phonenumber)
        {
            return await _dbContext.Users.AnyAsync(t => t.Id != id && t.PhoneNumber.ToLower() == phonenumber.ToLower());
        }
        public async Task<bool> ValidateUniquePhone1(string phonenumber)
        {
            return await _dbContext.Users.AnyAsync(t => t.PhoneNumber.ToLower() == phonenumber.ToLower());
        }
    }
}
