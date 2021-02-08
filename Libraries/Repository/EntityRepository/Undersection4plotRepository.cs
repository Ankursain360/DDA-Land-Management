using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.EntityRepository
{
    public class Undersection4plotRepository :  GenericRepository<Undersection4plot>, IUnderSection4PlotRepository
    {
        public Undersection4plotRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Undersection4>> GetAllNotificationNo()
        {
            List<Undersection4> notificationList = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return notificationList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra()
       {
           List<Khasra> KhasraList = await _dbContext.Khasra.Where(x=>x.IsActive==1).ToListAsync();
            return KhasraList;
        }


        public async Task<List<Undersection4plot>> GetAllUndersection4Plot()
        {
            return await _dbContext.Undersection4plot.Include(x => x.UnderSection4).Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Undersection4plot>> GetPagedNoUndersection4plot(NotificationUndersection4plotDto model)
        {
            return await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                .Include(x=>x.Village).Include(x=>x.Khasra)
                .OrderByDescending(x => x.Id).GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);
        }



    }
}
