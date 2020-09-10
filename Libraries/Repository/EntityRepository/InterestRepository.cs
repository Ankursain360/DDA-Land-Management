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
    public class InterestRepository : GenericRepository<Interest>, IInterestRepository
    {

        public InterestRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Interest>> GetAllDetails()
        {
            var data = await _dbContext.Interest.Include(s => s.PropertyType).ToListAsync();
            return data;
        }
        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.Propertytype.ToListAsync();
            return propertyTypeList;
        }
    }


}
