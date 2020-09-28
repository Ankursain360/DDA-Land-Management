using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class Undersection17Repository : GenericRepository<Undersection17>, IUndersection17Repository
    {
        public Undersection17Repository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> notificationList = await _dbContext.LandNotification.ToListAsync();
            return notificationList;
        }

        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            List<Undersection6> undersection6list = await _dbContext.Undersection6.ToListAsync();
            return undersection6list;
        }




        public async Task<List<Undersection17>> GetAllUndersection17()
        {
            return await _dbContext.Undersection17.Include(x => x.LandNotification).Include(x => x.Undersection6).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
