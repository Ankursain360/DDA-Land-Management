using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
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
            return await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<District>> GetPagedDistrict(DistrictSearchDto model)
        {
            return await _dbContext.District.Where(x => x.IsActive == 1).GetPaged<District>(model.PageNumber, model.PageSize);
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.District.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

    }
}
