using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandannexure2Service
    {
        Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model);
        Task<List<Newlandannexure2>> GetAllNewlandannexure2();
        Task<bool> Create(Newlandannexure2 Annexure2);
        Task<List<Newlandannexure2>> CheckReqExists(int id);
        Task<List<Newlandannexure2>> FetchSingleResultForReqId(int id, int UserId);
        Task<List<Newlandannexure2>> FetchSingleResult(int id);
        Task<Newlandannexure2> FetchSingleResultAnnx2(int id);
        Task<bool> Update(int id, Newlandannexure2 newlandannexure2);
        string GetS7Download(int id);
        string GetS8Download(int id);
        string GetS9Download(int id);
        string GetS12Download(int id);
    }
}
