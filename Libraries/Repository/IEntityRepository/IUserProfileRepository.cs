using Dto.Search;
using Libraries.Repository.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IEntityRepository
{
    public interface IUserProfileRepository : IGenericRepository<Userprofile>
    {
        Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model);
        Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model);
    }
}
