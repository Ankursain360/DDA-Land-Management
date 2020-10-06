using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILandTransferRepository : IGenericRepository<LandTransfer>
    {
        Task<List<LandTransfer>> GetAllLandTransfer();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<LandTransfer>> GetPagedLandTransfer(LandTransferSearchDto model);
    }
}
