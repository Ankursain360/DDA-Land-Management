using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEncroachmentRegisterationRepository:IGenericRepository<EncroachmentRegisteration>
    {
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Khasra>> GetAllKhasraList(int localityId);
    }
}
