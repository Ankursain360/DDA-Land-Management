using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPageRoleRepository: IGenericRepository<PageRole>
    {
        Task<List<PageRole>> GetAllPageRole();
        Task<List<Module>> GetAllModule();
        Task<List<Role>> GetAllRole();
        Task<List<User>> GetAllUser(int role);
    }
}
