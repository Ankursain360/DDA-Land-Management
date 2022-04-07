using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class DamageCalculationRepository : GenericRepository<Damagecalculation>, IDamageCalculationRepository
    {
        public DamageCalculationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<PropertyType>> GetPropertyType()
        {
            var propertytypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertytypeList;
        }
        public async Task<List<Locality>> GetLocalities()
        {
            //Zone id 59 only select damage payee locality list
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1 && x.ZoneId==59).ToListAsync();
            return localityList;
        }

        public async Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1)
        {
            return await  _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= date1 && x.EncroachEndDate >= date1)
                                .FirstOrDefaultAsync();
        }
        public async Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypea
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypeb
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypec
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }

        public async Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypeb
                                   .Where(x => ((x.StartDate <= dateTimeSpecific && x.EndDate >= dateTimeSpecific)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }

        public async Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypea
                                    .Where(x => ((x.StartDate <= specificDateTime && x.EndDate >= specificDateTime)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }

        public async Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1)
        {
            return await _dbContext.Comencrochmenttype
                               .Where(x => x.EncroachStartDate <= date1 && x.EncroachEndDate >= date1)
                               .FirstOrDefaultAsync();
        }
        public async Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypea
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypeb
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypec
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypeb
                                   .Where(x => ((x.StartDate <= dateTimeSpecific && x.EndDate >= dateTimeSpecific)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }

        public async Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypea
                                    .Where(x => ((x.StartDate <= specificDateTime && x.EndDate >= specificDateTime)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }
    }
}
