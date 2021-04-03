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
    public interface ILeaseApplicationFormService : IEntityService<Leaseapplication>
    {
        Task<bool> Create(Leaseapplication leaseapplication);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments);
        Task<Leaseapplication> FetchLeaseApplicationDetails(int id);
        Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id);
        Task<PagedResult<Leaseapplication>> GetPagedAllotmentLetter(DocumentChecklistSearchDto model);
        Task<bool> UpdateBeforeApproval(int id, Leaseapplication leaseapplication);
        Task<Leaseapplicationdocuments> FetchLeaseApplicationDocumentDetails(int id);
        Task<List<Allotmententry>> GetRefNoListforAllotmentLetter();
        Task<Allotmententry> FetchLeaseApplicationDetailsforAllotmentLetter(int id);
    }
}
