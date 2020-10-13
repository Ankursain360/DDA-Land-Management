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
        public UserProfileService(IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository) 
            : base(unitOfWork, userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model)
        {
            return await _userProfileRepository.GetPagedUser(model);
        }
    }
}
