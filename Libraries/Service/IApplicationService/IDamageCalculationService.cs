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
    public interface IDamageCalculationService : IEntityService<Damagecalculation>
    {
        Task<List<PropertyType>> GetPropertyTypes();
        Task<List<Locality>> GetLocalities();
        Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1);
        Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId);
    }
}
