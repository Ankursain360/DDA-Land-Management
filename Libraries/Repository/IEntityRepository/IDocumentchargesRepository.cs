

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDocumentchargesRepository : IGenericRepository<Documentcharges>
    {
        Task<List<Documentcharges>> GetAllDocumentcharges();
        Task<List<Documentcharges>> GetAllDocumentchargesList();
        //Task<List<PropertyType>> GetAllPropertyType();
        Task<PagedResult<Documentcharges>> GetPagedDocumentcharges(DocumentchargesSearchDto model);
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
    }
}

