using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDamageRateListService : IEntityService<Resratelisttypea>
    {
        Task<List<PropertyType>> GetPropertyTypes();
        Task<List<Locality>> GetLocalities();
        Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListResidential();
        Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListCommercial();
        Task<List<DamageRateListDataDto>> GetSearchResultPagedData(DamageRateListSearchDto model, List<DamageRateListDataDto> damageRateListDataDtos);
    }
}
