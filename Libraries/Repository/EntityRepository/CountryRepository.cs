using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Country>> GetCountry(){
            return await _dbContext.Country.ToListAsync();
        }
    }
}