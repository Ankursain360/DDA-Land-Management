using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDamageRateListRepository : IGenericRepository<Resratelisttypea>
    {
        Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListCommercial();
        Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListResidential();
        Task<List<Locality>> GetLocalities();
        Task<List<PropertyType>> GetPropertyTypes();
        Task<List<DamageRateListDataDto>> GetSearchResultPagedData(DamageRateListSearchDto model, List<DamageRateListDataDto> damageRateListDataDtos);
    }
}