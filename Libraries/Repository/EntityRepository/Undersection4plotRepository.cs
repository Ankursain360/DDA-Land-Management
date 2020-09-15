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

namespace Libraries.Repository.EntityRepository
{
    public class Undersection4plotRepository :  GenericRepository<Undersection4plot>, IUnderSection4PlotRepository
    {
        public Undersection4plotRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Undersection4>> GetAllNotificationNo()
        {
            List<Undersection4> notificationList = await _dbContext.Undersection4.ToListAsync();
            return notificationList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.ToListAsync();
            return villageList;
        }

        public async Task<List<Undersection4plot>> GetAllUndersection4Plot()
        {
            return await _dbContext.Undersection4plot.Include(x => x.NotificationNo).Include(x => x.VillageName).OrderByDescending(x => x.Id).ToListAsync();
        }





    }
}
