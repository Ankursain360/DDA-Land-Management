using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetUser();
        Task<List<Department>> GetAllDepartment();
        Task<List<Role>> GetAllRole();
        Task<bool> AnyLoginName(int id, string loginname);
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<PagedResult<User>> GetPagedUser(UserManagementSearchDto model);

        //Task<bool> MatchPassword(int id, string password);
    }
}
