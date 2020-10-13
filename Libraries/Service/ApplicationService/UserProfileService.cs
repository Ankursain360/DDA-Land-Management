using AutoMapper;
using Dto.Master;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserProfileService(IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper) 
            : base(unitOfWork, userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
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
    }
}
