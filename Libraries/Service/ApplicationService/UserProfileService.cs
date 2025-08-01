﻿using AutoMapper;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Identity;
using Model.Entity;
using Repository.IEntityRepository;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class UserProfileService : EntityService<Userprofile>, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserProfileService(IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
            : base(unitOfWork, userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Userprofile>> GetAllUser()
        {
            return await _userProfileRepository.GetAllUser();
        }
        public async Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model)
        {
            var role = await _userProfileRepository.GetPagedRole(model);
            return role;
        }

        public async Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model)
        {
            return await _userProfileRepository.GetPagedUser(model);
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<RoleDto>(role);
            return result;
        }

        public async Task<List<UserProfileDto>> GetUser()
        {
            var user = await _userProfileRepository.GetUser();
            var result = _mapper.Map<List<UserProfileDto>>(user);
            return result;
        }

        public async Task<List<RoleDto>> GetRole()
        {
            var role = await _userProfileRepository.GetRole();
            return _mapper.Map<List<RoleDto>>(role);
        }

        public async Task<List<RoleDto>> GetActiveRole()
        {
            var role = await _userProfileRepository.GetActiveRole();
            return _mapper.Map<List<RoleDto>>(role);
        }

        public async Task<UserProfileDto> GetUserById(int userId)
        {
            var user = await _userProfileRepository.GetUserById(userId);
            var result = _mapper.Map<UserProfileDto>(user);
            return result;
        }

        public async Task<UserProfileDto> GetUserProfileById(int userId)
        {
            var user = await _userProfileRepository.GetUserProfileById(userId);
            var result = _mapper.Map<UserProfileDto>(user);
            return result;
        }
        




        public async Task<List<KycApplicationSearchDto>> KycApplicationDetails(int id)
        {
            return await _userProfileRepository.KycApplicationDetails(id);
        }

        public async Task<List<KycDemandPaymentSearchDto>> KycDemandPaymentDetails(int id)
        {
            return await _userProfileRepository.KycDemandPaymentDetails(id);
        }
        public async Task<bool> UpdateRole(RoleDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            role.Name = roleDto.Name;
            role.IsActive = roleDto.IsActive;
            role.ModifiedBy = 1;
            role.ModifiedDate = DateTime.Now;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> DeleteRole(RoleDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            // role.Name = roleDto.Name;
            role.IsActive = 0;
            role.ModifiedBy = 1;
            role.ModifiedDate = DateTime.Now;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> CreateUser(AddUserDto userDto)
        {
            int profileSaveResult = 0;
            ApplicationUser user = new ApplicationUser()
            {
                Name = userDto.Name,
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordSetDate = DateTime.Now.AddDays(30),
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                IsDefaultPassword = 1
            };

            var userSavedResult = await _userManager.CreateAsync(user, userDto.Password);
            if (userSavedResult.Succeeded)
            {
                var savedUser = await _userManager.FindByNameAsync(user.UserName);
                Userprofile userprofile = new Userprofile()
                {
                    ZoneId = userDto.ZoneId,
                    DepartmentId = userDto.DepartmentId,
                    BranchId = userDto.BranchId,
                    DistrictId = userDto.DistrictId,
                    RoleId = userDto.RoleId,
                    IsActive = 1,
                    UserId = savedUser.Id,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                };
                _userProfileRepository.Add(userprofile);
                profileSaveResult = await _unitOfWork.CommitAsync();
            }

            return profileSaveResult > 0;
        }

        public async Task<bool> UpdateUser(EditUserDto userDto)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Name = userDto.Name;
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> ValidateUniqueRoleName(int id, string name)
        {
            return await _userProfileRepository.ValidateUniqueRoleName(id, name);
        }
        public async Task<bool> ValidateUniqueUserName(int id, string UserName)
        {
            return await _userProfileRepository.ValidateUniqueUserName(id, UserName);
        }
        public async Task<bool> ValidateUniqueUserName1(string Name)
        {
            return await _userProfileRepository.ValidateUniqueUserName1(Name);
        }

        public async Task<bool> UpdateUserPersonalDetails(UserPersonalInfoDto model)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.UserName = model.UserName;
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> UpdateUserProfileDetails(UserProfileEditDto model)
        {
            int profileSaveResult = 1;
            Userprofile user = await _userProfileRepository.GetUserById(model.Id);
            if (user.DepartmentId != model.DepartmentId || user.RoleId != model.RoleId || user.ZoneId != model.ZoneId || user.BranchId != model.BranchId)
            {
                user.IsActive = 0;
                _userProfileRepository.Edit(user);
                profileSaveResult = await _unitOfWork.CommitAsync();

                user.Id = 0;
                user.DepartmentId = model.DepartmentId;
                user.BranchId = model.BranchId;
                user.RoleId = model.RoleId;
                user.ZoneId = model.ZoneId;
                user.IsActive = 1;
                user.UserId = model.Id;
                user.CreatedBy = 1;
                user.CreatedDate = DateTime.Now;
                _userProfileRepository.Add(user);
                profileSaveResult = await _unitOfWork.CommitAsync();
            }

            //user.DepartmentId = model.DepartmentId;
            //user.RoleId = model.RoleId;
            //user.ZoneId = model.ZoneId;
            //  user.DistrictId = model.DistrictId;
            return profileSaveResult > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            Userprofile user = await _userProfileRepository.GetUserById(id);
            user.IsActive = 0;
            _userProfileRepository.Edit(user);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<ZoneDto>> GetAllZone(int departmentId)
        {
            var zones = await _userProfileRepository.GetAllZone(departmentId);
            var result = _mapper.Map<List<ZoneDto>>(zones);
            return result;
        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _userProfileRepository.GetAllotteeDetails(userId);
        }

        public async Task<List<UserProfileDto>> GetUserOnRoleBasis(int roleId)
        {
            var user = await _userProfileRepository.GetUserOnRoleBasis(roleId);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            return result;
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserSkippingItsOwnConcatedName(int roleId, int userid)
        {
            var user = await _userProfileRepository.GetUserSkippingItsOwnConcatedName(roleId, userid);
            return user;
        }
        public async Task<List<kycUserProfileInfoDetailsDto>> GetkycUserSkippingItsOwnConcatedName(int roleId, int userid)//added by ishu 23/7/2021
        {
            var user = await _userProfileRepository.GetkycUserSkippingItsOwnConcatedName(roleId, userid);
            return user;
        }
        public async Task<List<UserWithRoleDto>> GetUserWithRole()
        {
            return await _userProfileRepository.GetUserWithRole();
        }

        public async Task<List<UserProfileDto>> GetUserOnRoleZoneBasis(int roleId, int zoneId)
        {
            var user = await _userProfileRepository.GetUserOnRoleZoneBasis(roleId, zoneId);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            return result;
        }

        public async Task<List<UserProfileDto>>  GetUserOnRoleBranchBasis(int roleId, int branchId)
        {
            var user = await _userProfileRepository.GetUserOnRoleBranchBasis(roleId, branchId);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            return result;
        }

        public async Task<UserProfileDto> GetUserByIdZone(int userid, int zoneId)
        {
            var user = await _userProfileRepository.GetUserByIdZone(userid, zoneId);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            if (result.Count == 0)
                return null;
            else
                return result[0];
        }
        public async Task<UserProfileDto> GetUserByIdBranch(int userid, int branchId)
        {
            var user = await _userProfileRepository.GetUserByIdBranch(userid, branchId);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            if (result.Count == 0)
                return null;
            else
                return result[0];
        }
        public async Task<List<UserProfileDto>> UserListSkippingmultiusers(int[] nums)
        {
            var user = await _userProfileRepository.UserListSkippingmultiusers(nums);
            var result = _mapper.Map<List<UserProfileDto>>(user);
            return result;
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleZoneBasisConcatedName(int roleId, int zoneId)
        {
            return await _userProfileRepository.GetUserOnRoleZoneBasisConcatedName(roleId , zoneId);
        }
        public async Task<List<kycUserProfileInfoDetailsDto>> kycGetUserOnRoleZoneBasisConcatedName(int roleId, int branchId)// added by ishu 23/7/2021
        {
            return await _userProfileRepository.kycGetUserOnRoleZoneBasisConcatedName(roleId, branchId);
        }

        public async Task<List<UserProfileInfoDetailsDto>> GetUserOnRoleBasisConcatedName(int roleId)
        {
            return await _userProfileRepository.GetUserOnRoleBasisConcatedName(roleId);
        }
        public async Task<List<kycUserProfileInfoDetailsDto>> GetkycUserOnRoleBasisConcatedName(int roleId)// added by ishu 23/7/2021
        {
            return await _userProfileRepository.GetkycUserOnRoleBasisConcatedName(roleId);
        }

        public async Task<UserProfileInfoDetailsDto> GetUserByIdZoneConcatedName(int userid, int zoneId)
        {
            var user = await _userProfileRepository.GetUserByIdZoneConcatedName(userid, zoneId);
            if (user.Count == 0)
                return null;
            else
                return user[0];
        }
        public async Task<kycUserProfileInfoDetailsDto> GetUserByIdBranchConcatedName(int userid, int branchId)//added by ishu 23/7/2021
        {
            var user = await _userProfileRepository.GetUserByIdBranchConcatedName(userid, branchId);
            if (user.Count == 0)
                return null;
            else
                return user[0];
        }

        public async Task<List<UserProfileInfoDetailsDto>> UserListSkippingmultiusersConcatedName(int[] nums)
        {
            return await _userProfileRepository.UserListSkippingmultiusersConcatedName(nums);
        }
        public async Task<List<kycUserProfileInfoDetailsDto>> kycUserListSkippingmultiusersConcatedName(int[] nums)//added by ishu 23/7/2021
        {
            return await _userProfileRepository.kycUserListSkippingmultiusersConcatedName(nums);
        }

        public async Task<bool> ValidateUniqueEmail(int id, string email)
        {
            return await _userProfileRepository.ValidateUniqueEmail(id, email);
        }
        public async Task<bool> ValidateUniqueEmail1( string email)
        {
            return await _userProfileRepository.ValidateUniqueEmail1(email);
        }
        public async Task<bool> ValidateUniquePhone(int id, string phonenumber)
        {
            return await _userProfileRepository.ValidateUniquePhone(id, phonenumber);
        }
        public async Task<bool> ValidateUniquePhone1(string phonenumber)
        {
            return await _userProfileRepository.ValidateUniquePhone1(phonenumber);
        }
    }
}
