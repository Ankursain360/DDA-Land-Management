using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IInterestrateRepository : IGenericRepository<Interestrate>
    {
        Task<List<Interestrate>> GetAllInterestrate();
        Task<List<PropertyType>> GetAllPropertyType();
        Task<PagedResult<Interestrate>> GetPagedInterestrate(InterestrateSearchDto model);
    }
}
