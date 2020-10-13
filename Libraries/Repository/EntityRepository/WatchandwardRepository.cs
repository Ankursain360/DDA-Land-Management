using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

  public class WatchandwardRepository : GenericRepository<Watchandward>, IWatchandwardRepository
    {
        public WatchandwardRepository(DataContext dbContext) : base(dbContext)
        {

        }
      

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model)
        {
            return await _dbContext.Watchandward.Where(x => x.IsActive == 1)
                .Include(x => x.Village)
                .Include(x => x.Khasra)
                .GetPaged<Watchandward>(model.PageNumber, model.PageSize);
        }



        public async Task<List<Watchandward>> GetWatchandward()
        {
            return await _dbContext.Watchandward.ToListAsync();
        }
        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _dbContext.Watchandward.Include(x => x.Village)
                .Include(x => x.Khasra)
                .ToListAsync();
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
            return villagelist;
        }

        public async Task<List<Watchandward>> GetWatchandwardReportData(int village,DateTime fromdate, DateTime todate)
        {
            var data = await _dbContext.Watchandward
                .Include(x => x.Village)
                .Include(x=>x.Khasra)             
                . Where(x => (x.VillageId == (village == 0 ? x.VillageId : village))
                && x.Date >= fromdate && x.Date<=todate).OrderByDescending(x => x.Id).ToListAsync();

            return data;
        }

    }
}
