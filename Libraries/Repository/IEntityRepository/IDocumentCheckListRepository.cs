using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDocumentCheckListRepository : IGenericRepository<Documentchecklist>
    {

        Task<bool> Any(int id, string name, int ServiceTypeId);
        Task<List<Servicetype>> GetServiceTypeList();
        Task<List<Documentchecklist>> GetAllDocumentchecklist();
        Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model);
        Task<Documentchecklist> FetchSingleResult(int id);
    }
}