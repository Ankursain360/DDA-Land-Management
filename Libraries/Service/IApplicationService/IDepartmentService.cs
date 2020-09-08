using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;



namespace Libraries.Service.IApplicationService
{
    public interface IDepartmentService : IEntityService<Department>
    {
        Task<List<Department>> GetAllDepartment(); // To Get all data added by renu
        Task<List<Department>> GetDepartmentUsingRepo();

        Task<bool> Update(int id, Department department); // To Upadte Particular data added by renu

        Task<bool> Create(Department department);

        Task<Department> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string department);   // To check Unique Value  for designation
    }
   
}
