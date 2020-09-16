using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDivisionRepository : IGenericRepository<Division>
    {
        Task<List<Division>> GetDivisions();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Department>> GetAllDepartment();

        Task<bool> Any(int id, string name);
    }
}
