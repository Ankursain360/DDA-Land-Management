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

    public interface IInterestrateService : IEntityService<Interestrate>
    {
        Task<List<Interestrate>> GetAllInterestrate();
        Task<List<PropertyType>> GetAllPropertyType();
        Task<bool> Update(int id, Interestrate rate);
        Task<bool> Create(Interestrate rate);
        Task<Interestrate> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Interestrate>> GetPagedInterestrate(InterestrateSearchDto model);

    }
}
