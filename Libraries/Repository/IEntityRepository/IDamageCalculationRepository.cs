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
        Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId);
        Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1);
        Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId);
    }
}
