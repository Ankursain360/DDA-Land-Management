using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IVillageRepository : IGenericRepository<Village>
    {
        Task<PagedResult<Village>> GetPagedVillage(VillageSearchDto model);
        Task<List<Village>> GetVillage();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<bool> Any(int id, string name);
        Task<List<Division>> GetAllDivisionList(int departmentId);
        Task<List<Department>> GetAllDepartmentList();
        Task<List<Zone>> GetAllZone();
    }
}
