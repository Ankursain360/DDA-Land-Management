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
  
    public class Undersection22plotdetailsRepository : GenericRepository<Undersection22plotdetails>, IUndersection22plotdetailsRepository
    {
        public Undersection22plotdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Undersection22plotdetails>> GetAllUS22PlotDetails()
        {
            return await _dbContext.Undersection22plotdetails
                                   .Include(x => x.UnderSection4)
                                   .Include(x => x.UnderSection6)
                                   .Include(x => x.UnderSection17)
                                   .Include(x => x.UnderSection22)
                                   .Include(x => x.Khasra)
                                   .Include(x => x.Acquiredlandvillage)
                                   .Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Acquiredlandvillage>> GetAllAcquiredlandvillage()
        {
            List<Acquiredlandvillage> acqvillageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return acqvillageList;
        }

        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Undersection4>> GetAllUndersection4()
        {
            List<Undersection4> us4List = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return us4List;
        }
        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            List<Undersection6> us6List = await _dbContext.Undersection6.Where(x => x.IsActive == 1).ToListAsync();
            return us6List;
        }
        public async Task<List<Undersection17>> GetAllUndersection17()
        {
            List<Undersection17> us17List = await _dbContext.Undersection17.Where(x => x.IsActive == 1).ToListAsync();
            return us17List;
        }
        public async Task<List<Undersection22>> GetAllUndersection22()
        {
            List<Undersection22> us22List = await _dbContext.Undersection22.Where(x => x.IsActive == 1).ToListAsync();
            return us22List;
        }

        //public async Task<PagedResult<Undersection22plotdetails>> GetPagedUndersection22plotdetails(Undersection22plotdetailsSearchDto model)
        //{

        //}
       
    }
}
