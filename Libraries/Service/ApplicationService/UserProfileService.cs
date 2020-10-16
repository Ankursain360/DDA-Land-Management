using AutoMapper;
using Dto.Master;
using Dto.Search;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserProfileService(IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper) 
            : base(unitOfWork, userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _roleManager = roleManager;
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

        public async Task<bool> UpdateRole(RoleDto model)
        {
            var role = _mapper.Map<ApplicationRole>(model);
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded ? true : false;
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

        public async Task<UserProfileDto> GetUserById(int userId)
        {
            var user = await _userProfileRepository.GetUserById(userId);
            var result = _mapper.Map<UserProfileDto>(user);
            return result;
        }
    }
}
