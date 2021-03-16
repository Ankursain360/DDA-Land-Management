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
        //Task<bool> Update(int id, Documentchecklist documentchecklist); // To Upadte Particular data added by renu

        Task<bool> Create(Leaseapplication leaseapplication);

        //Task<Documentchecklist> FetchSingleResult(int id);  // To fetch Particular data added by renu

        //Task<bool> Delete(int id);    // To Delete Data  added by renu

        //Task<bool> CheckUniqueName(int id, string zone, int ServiceTypeId);// To check Unique Value  for zone
      //  Task<List<Servicetype>> GetServiceTypeList();
        //Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model);
        Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid);
        Task<bool> SaveLeaseApplicationDocuments(List<Leaseapplicationdocuments> leaseapplicationdocuments);
        Task<Leaseapplication> FetchLeaseApplicationDetails(int id);
        Task<List<Leaseapplicationdocuments>> LeaseApplicationDocumentDetails(int id);
        Task<PagedResult<Leaseapplication>> GetPagedAllotmentLetter(DocumentChecklistSearchDto model);
        Task<bool> UpdateBeforeApproval(int id, Leaseapplication leaseapplication);
        Task<Leaseapplicationdocuments> FetchLeaseApplicationDocumentDetails(int id);
    }
}
