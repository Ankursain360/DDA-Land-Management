using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using Service.IApplicationService;
using System;
using System.Collections.Generic;

namespace Service.ApplicationService
{
    public class UserProfileService : EntityService<Userprofile>, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork, IGenericRepository<Userprofile> repository) : base(unitOfWork, repository)
        {
        }
    }
}
