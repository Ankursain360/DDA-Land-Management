using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.IEntityRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                                    .Where(a=> (string.IsNullOrEmpty(model.UserName) || a.User.UserName.Contains(model.UserName))
                                        && (string.IsNullOrEmpty(model.Name) || a.User.Name.Contains(model.Name))
                                        && (string.IsNullOrEmpty(model.Email) || a.User.Email.Contains(model.Email))
                                        && (string.IsNullOrEmpty(model.PhoneNumber) || a.User.PhoneNumber.Contains(model.PhoneNumber))
                                        && (a.IsActive == 1)
                                    )
                                    .GetPaged<Userprofile>(model.PageNumber, model.PageSize);
            return result;
        }

        public async Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model)
        {
            var result = await _dbContext.Roles.
                            GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
            return result;
        }

        public async Task<List<ApplicationUser>> GetUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<ApplicationRole>> GetRole()
        {
            return await _dbContext.Roles.ToListAsync();
        }
    }
}
