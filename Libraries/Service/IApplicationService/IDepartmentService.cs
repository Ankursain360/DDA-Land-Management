using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDepartmentService : IEntityService<Department>
    {
        Task<List<Department>> GetAllDepartment();
        Task<List<Department>> GetDepartmentnUsingRepo();

        Task<bool> Update(int id, Department department);
    }
   
}
