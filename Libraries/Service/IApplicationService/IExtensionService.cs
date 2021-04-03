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
    public interface IExtensionService : IEntityService<Mortgage>
    {
        Task<PagedResult<Mortgage>> GetPagedMortgageDetails(MortgageSearchDto model);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> Create(Mortgage mortgage);
        Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments);
        Task<Possesionplan> GetAllotteeDetails(int userId);
    }
}
