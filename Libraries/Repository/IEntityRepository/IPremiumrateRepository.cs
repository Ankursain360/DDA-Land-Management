using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
     
    public interface IPremiumrateRepository : IGenericRepository<Premiumrate>
    {
        Task<List<Premiumrate>> GetAllPremiumrate();
        Task<List<Premiumrate>> GetAllPremiumrateList();
        //Task<List<PropertyType>> GetAllPropertyType();
        Task<PagedResult<Premiumrate>> GetPagedPremiumrate(PremiumrateSearchDto model);
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);

    }
}
