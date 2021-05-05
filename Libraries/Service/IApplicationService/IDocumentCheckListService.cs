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
    public interface IDocumentCheckListService : IEntityService<Documentchecklist>
    {
        Task<bool> Update(int id, Documentchecklist documentchecklist); // To Upadte Particular data added by renu

        Task<bool> Create(Documentchecklist documentchecklist);

        Task<Documentchecklist> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string zone,int ServiceTypeId);// To check Unique Value  for zone
        Task<List<Servicetype>> GetServiceTypeList();
        Task<List<Documentchecklist>> GetAllDocumentchecklist();
        Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model);
    }
}
