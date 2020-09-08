using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
  public  class DistrictRepository:GenericRepository<District>, IDistrictRepository
    {
        public DistrictRepository(DataContext dbcontext):base(dbcontext)
        { }

        public async Task<List<District>> GetDistricts()
        {
            return await _dbContext.District.ToListAsync();
        }


    }
}
