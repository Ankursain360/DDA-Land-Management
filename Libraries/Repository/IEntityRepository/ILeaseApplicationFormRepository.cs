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
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments);
        Task<Leaseapplication> FetchLeaseApplicationDetails(int id);
        Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id);
        Task<PagedResult<Leaseapplication>> GetPagedAllotmentLetter(DocumentChecklistSearchDto model);
        Task<Leaseapplicationdocuments> FetchLeaseApplicationDocumentDetails(int id);
        Task<List<Allotmententry>> GetRefNoListforAllotmentLetter();
        Task<Allotmentletter> FetchLeaseApplicationDetailsforAllotmentLetter(int id);
    }
}