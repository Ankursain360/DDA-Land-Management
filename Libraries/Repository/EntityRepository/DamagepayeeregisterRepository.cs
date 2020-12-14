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
    public class DamagepayeeregisterRepository : GenericRepository<Damagepayeeregister>, IDamagepayeeregisterRepository
    {
        public DamagepayeeregisterRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model)
        {
            return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .Include(x => x.Locality)
                .Include(x => x.District)
                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Damagepayeeregister>> GetDamagepayeeregister()
        {
            return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .ToListAsync();
        }
       

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
        {
                 return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .Include(x => x.Locality)
                .Include(x => x.District)
                .ToListAsync();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            var districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
        }

    }
}
