

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IDocumentchargesServices : IEntityService<Documentcharges>
    {
        Task<List<Documentcharges>> GetAllDocumentcharges();
        //Task<List<PropertyType>> GetAllPropertyType();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId);
        Task<bool> Update(int id, Documentcharges charge);
        Task<bool> Create(Documentcharges charge);
        Task<Documentcharges> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Documentcharges>> GetPagedDocumentcharges(DocumentchargesSearchDto model);
        Task<List<Documentcharges>> GetAllDocumentchargesList();

    }
}
