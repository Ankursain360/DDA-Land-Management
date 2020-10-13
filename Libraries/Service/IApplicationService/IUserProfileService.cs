using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IUserProfileService : IEntityService<Userprofile>
    {
        Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model);
        Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model);
    }
}
