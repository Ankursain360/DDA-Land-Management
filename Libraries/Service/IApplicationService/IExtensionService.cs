using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IExtensionService : IEntityService<Extension>
    {
        Task<PagedResult<Extension>> GetPagedExtensionServiceDetails(ExtensionServiceSearchDto model);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> Create(Extension extensionservice);
        Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<Timeextension> GetTimeLineExtensionFees();
        Task<Extension> FetchSingleResult(int id);
        Task<Allotteeservicesdocument> FetchSingleResultDocument(int id);
        Task<bool> UpdateBeforeApproval(int id, Extension extension);
        Task<bool> Update(int id, Extension extension);
        Task<bool> Delete(int id, int userId);
        Task<List<Allotteeservicesdocument>> AlloteeDocumentListDetails(int id, int servicetypeid);
        Task<bool> UpdateAllotteeServiceDocuments(int id, Allotteeservicesdocument allotteeservicesdocuments);
        Task<bool> SaveAllotteeServiceDocumentsSingle(Allotteeservicesdocument item);
        Task<Extension> IsNeedAddMore();
    }
}
