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
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }

        public async Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1)
        {
            return await  _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate >= date1 && x.EncroachEndDate <= date1)
                                .FirstOrDefaultAsync();
        }
        public async Task<Resratelisttypea> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypea
                                    .Where(x => x.StartDate >= date1 && x.EndDate <= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .FirstOrDefaultAsync();
        }
        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownListMOR()
        {
            var badCodes = new[] { 3, 5 };
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland
                                                                        .Where(x => x.IsActive == 1 && badCodes.Contains(x.Id))
                                                                        .ToListAsync();
            return ClassificationoflandList;
        }
    }
}
