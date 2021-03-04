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
    }
}
