using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEncroachmentRegisterationApprovalRepository : IGenericRepository<EncroachmentRegisteration>
    {
        Task<List<EncroachmentRegisteration>> GetEncroachmentRegisteration();
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Locality>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterApprovalSearchDto model, int userId, int zoneId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
