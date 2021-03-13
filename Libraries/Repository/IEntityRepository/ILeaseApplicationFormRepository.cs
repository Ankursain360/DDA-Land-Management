using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeaseApplicationFormRepository : IGenericRepository<Leaseapplication>
    {

        //Task<bool> Any(int id, string name, int ServiceTypeId);
        //Task<List<Servicetype>> GetServiceTypeList();
        //Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model);
        //Task<Documentchecklist> FetchSingleResult(int id);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments);
        Task<Leaseapplication> FetchLeaseApplicationDetails(int id);
        Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id);
    }
}