using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDamageCalculationRepository : IGenericRepository<Damagecalculation>
    {

        Task<List<PropertyType>> GetPropertyType();
        Task<List<Locality>> GetLocalities();
        Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1);
        Task<Resratelisttypea> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
    }
}
