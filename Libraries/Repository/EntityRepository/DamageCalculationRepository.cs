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
    }
}
