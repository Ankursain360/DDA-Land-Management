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
    public interface IOldAllotmentEntryService : IEntityService<Leaseapplication>
 
    {
        Task<bool> Create(Leaseapplication lease);
        Task<List<Leasetype>> GetAllLeaseType();
        Task<List<PropertyType>> GetAllPropertyType();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
    }
}
