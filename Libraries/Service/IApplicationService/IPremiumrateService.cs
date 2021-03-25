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
   
    public interface IPremiumrateService : IEntityService<Premiumrate>
    {
        Task<List<Premiumrate>> GetAllPremiumrate();
        //Task<List<PropertyType>> GetAllPropertyType();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId);
        Task<bool> Update(int id, Premiumrate rate);
        Task<bool> Create(Premiumrate rate);
        Task<Premiumrate> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Premiumrate>> GetPagedPremiumrate(PremiumrateSearchDto model);

    }
}
