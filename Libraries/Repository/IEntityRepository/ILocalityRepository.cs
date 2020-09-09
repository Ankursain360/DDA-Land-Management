using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILocalityRepository : IGenericRepository<Locality>
    {
        Task<List<Locality>> GetLocality();
        Task<List<Zone>> GetAllZone();
        Task<List<Department>> GetAllDepartment();
        Task<bool> Any(int id, string name);
    }
}
