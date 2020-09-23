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
        Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId, int? userId);
        Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId);
        Task<bool> Add(AssignPageRoleWise assignPageRoleWise);
        Task<bool> DeletePageRole(PageRole pageRole);
        Task<bool> DeleteAssignPageRoleWise(AssignPageRoleWise assignPageRoleWise);
        Task<bool> AddPageRole(PageRole pageRole);
    }
}
