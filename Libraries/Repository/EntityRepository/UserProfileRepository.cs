using Dto.Master;
using Libraries.Model;
using Libraries.Repository.Common;
using Model.Entity;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dto.Search;

namespace Repository.EntityRepository
{
    public class UserProfileRepository : GenericRepository<Userprofile>, IUserProfileRepository
    {
        public UserProfileRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model) {
            var result = await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .GetPaged<Userprofile>(model.PageNumber, model.PageSize);
            return result;
        }
    }
}
