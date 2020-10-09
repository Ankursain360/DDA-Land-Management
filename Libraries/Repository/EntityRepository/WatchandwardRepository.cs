using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

  public class WatchandwardRepository : GenericRepository<Watchandward>, IWatchandwardRepository
    {
        public WatchandwardRepository(DataContext dbContext) : base(dbContext)
        {

        }
        //public async Task<PagedResult<Page>> GetPagedPage(PageSearchDto model)
        //{
        //    return await _dbContext.Page.GetPaged<Page>(model.PageNumber, model.PageSize);
        //}
        public async Task<List<Watchandward>> GetWatchandward()
        {
            return await _dbContext.Watchandward.ToListAsync();
        }
        //public async Task<List<Page>> GetAllPage()
        //{
        //    return await _dbContext.Page.Include(x => x.Module).ToListAsync();
        //}
        //public async Task<List<Module>> GetAllModule()
        //{
        //    List<Module> moduleList = await _dbContext.Module.ToListAsync();
        //    return moduleList;
        //}


    }
}
