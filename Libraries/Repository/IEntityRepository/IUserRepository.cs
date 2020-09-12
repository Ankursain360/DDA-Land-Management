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
        Task<List<District>> GetAllDistrict();
        Task<List<Role>> GetAllRole();
        Task<bool> AnyLoginName(int id, string loginname);

        //Task<bool> MatchPassword(int id, string password);
    }
}
