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
    }
}
