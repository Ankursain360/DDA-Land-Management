using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetUser();
        Task<List<Department>> GetAllDepartment();
        Task<List<Role>> GetAllRole();
        Task<bool> AnyLoginName(int id, string loginname);
        Task<List<Zone>> GetAllZone(int departmentId);

        //Task<bool> MatchPassword(int id, string password);
    }
}
