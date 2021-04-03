using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IExtensionRepository : IGenericRepository<Extension>
    {
        Task<PagedResult<Extension>> GetPagedExtensionServiceDetails(ExtensionServiceSearchDto model);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<Timeextension> GetTimeLineExtensionFees();
        Task<Extension> FetchSingleResult(int id);
        Task<Allotteeservicesdocument> FetchSingleResultDocument(int id);
        Task<List<Allotteeservicesdocument>> AlloteeDocumentListDetails(int id);
        Task<bool> UpdateAllotteeServiceDocuments(int id, Allotteeservicesdocument allotteeservicesdocuments);
        Task<bool> SaveAllotteeServiceDocumentsSingle(Allotteeservicesdocument item);
    }
}