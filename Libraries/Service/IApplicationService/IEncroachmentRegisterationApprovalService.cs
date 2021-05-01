using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IEncroachmentRegisterationApprovalService : IEntityService<EncroachmentRegisteration>
    {
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Khasra>> GetAllKhasra();
        Task<List<Village>> GetAllVillage();
        Task<List<Locality>> GetAllLocality();
        Task<EncroachmentRegisteration> FetchSingleResult(int id);
        Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterApprovalSearchDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
