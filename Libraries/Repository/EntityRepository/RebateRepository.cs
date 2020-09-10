using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class RebateRepository : GenericRepository<Rebate>, IRebateRepository
    {

        public RebateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        
        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyTypeList;
        }
    }


}
