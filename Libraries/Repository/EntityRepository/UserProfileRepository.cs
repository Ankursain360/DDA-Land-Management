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

        public async Task<PagedResult<Userprofile>> GetPagedUser(UserManagementSearchDto model)
        {

            var data = await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => (string.IsNullOrEmpty(model.UserName) || a.User.UserName.Contains(model.UserName))
                                        && (string.IsNullOrEmpty(model.Name) || a.User.Name.Contains(model.Name))
                                        && (string.IsNullOrEmpty(model.Email) || a.User.Email.Contains(model.Email))
                                        && (string.IsNullOrEmpty(model.PhoneNumber) || a.User.PhoneNumber.Contains(model.PhoneNumber))
                                        && (a.IsActive == 1)
                                    )
                                    .GetPaged<Userprofile>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.User.Name).ToList();
                        break;
                    case ("USERNAME"):
                        data.Results = data.Results.OrderBy(x => x.User.UserName).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderBy(x => x.User.Email).ToList();
                        break;
                    case ("PHONENUMBER"):
                        data.Results = data.Results.OrderBy(x => x.User.PhoneNumber).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.User.Name).ToList();
                        break;
                    case ("USERNAME"):
                        data.Results = data.Results.OrderByDescending(x => x.User.UserName).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderByDescending(x => x.User.Email).ToList();
                        break;
                    case ("PHONENUMBER"):
                        data.Results = data.Results.OrderByDescending(x => x.User.PhoneNumber).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            
            return data;
        }

        public async Task<PagedResult<ApplicationRole>> GetPagedRole(RoleSearchDto model)
        {
            var data = await _dbContext.Roles
                           .Where(x =>string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                           .OrderByDescending(s => s.IsActive)
                       .GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
           
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;

            //var result = await _dbContext.Roles.
            //                GetPaged<ApplicationRole>(model.PageNumber, model.PageSize);
            //return result;
        }

        public async Task<List<Userprofile>> GetUser()
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => a.IsActive == 1)
                                    .ToListAsync();
        }

        public async Task<Userprofile> GetUserById(int userId)
        {
            return await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a => a.IsActive == 1 && a.User.Id == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<ApplicationRole>> GetRole()
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<List<ApplicationRole>> GetActiveRole()
        {
            return await _dbContext.Roles.Where(a => a.IsActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<bool> ValidateUniqueRoleName(int id, string name)
        {
            return await _dbContext.Roles.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> ValidateUniqueUserName(int id, string userName)
        {
            return await _dbContext.Users.AnyAsync(t => t.Id != id && t.Name.ToLower() == userName.ToLower());
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
            return zoneList;
        }
    }
}
