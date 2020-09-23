using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IPageRoleService : IEntityService<PageRole>
    {
        Task<List<PageRole>> GetAllPageRole(); // To Get all data added by Praveen
        Task<List<Module>> GetAllModuleList(); // To Get all data added by Praveen
        Task<List<Role>> GetAllRoleList(); // To Get all data added by Praveen
        Task<List<User>> GetUserList(int Role); // To Get all data added by Praveen
        Task<bool> Create(PageRole pageRole);
        Task<bool> DeletePageRole(PageRole pageRole);
        Task<bool> DeleteAssignPageRoleWise(AssignPageRoleWise assignPageRoleWise);
        Task<bool> Create(AssignPageRoleWise assignPageRoleWise);
        Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId);
        Task<List<PageRole>> GetPageRoleDetailsUserWise(int moduleId, int roleId, int? userId);
        Task<bool> CreatePageRole(PageRole pageRole);
    }
}
