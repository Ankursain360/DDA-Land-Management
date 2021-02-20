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
    public class Newlandus4plotRepository : GenericRepository<Newlandus4plot>, INewlandus4plotRepository
    {

        public Newlandus4plotRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandus4plot>> GetPagedUS4Plot(Newlandus4plotSearchDto model)
        {
            var data = await _dbContext.Newlandus4plot
                                  .Include(x => x.Village)
                                  .Include(x => x.Notification)
                                  .Include(x => x.Khasra)
                                  .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                   && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                   && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                  .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);
            return data;
        }
        public async Task<List<Newlandus4plot>> GetAllUS4Plot()
        {
            return await _dbContext.Newlandus4plot
                                   .Include(x => x.Village)
                                   .Include(x => x.Khasra)
                                   .Include(x => x.Notification)
                                   .ToListAsync();
        }

        public async Task<List<LandNotification>> GetAllNotification()
        {
            List<LandNotification> notificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1).ToListAsync();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }
    }
}
