using AutoMapper;
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
            if (user.DepartmentId != model.DepartmentId || user.RoleId != model.RoleId || user.ZoneId != model.ZoneId)
            {
                user.IsActive = 0;
                _userProfileRepository.Edit(user);
                profileSaveResult = await _unitOfWork.CommitAsync();

                user.Id = 0;
                user.DepartmentId = model.DepartmentId;
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
        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _userProfileRepository.GetAllZone(departmentId);
            return zoneList;
        }

    }
}
