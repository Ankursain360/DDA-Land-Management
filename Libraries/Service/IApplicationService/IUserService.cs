using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUser(); 
        Task<List<Department>> GetAllDepartment(); 
        Task<List<Role>> GetAllRole(); 
        Task<List<User>> GetUserUsingRepo();
        Task<bool> Update(int id, User user); 
        Task<bool> Create(User user);
        Task<User> FetchSingleResult(int id); 
        Task<bool> Delete(int id);   
        Task<bool> CheckUniqueLoginName(int id, string loginname);
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<PagedResult<User>> GetPagedUser(UserManagementSearchDto model);

    }
}
