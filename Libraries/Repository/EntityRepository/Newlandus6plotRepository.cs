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
    public class Newlandus6plotRepository : GenericRepository<Newlandus6plot>, INewlandus6plotRepository
    {

        public Newlandus6plotRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandus6plot>> GetPagedUS6Plot(Newlandus6plotSearchDto model)
        {
            var data = await _dbContext.Newlandus6plot
                                  .Include(x => x.Village)
                                  .Include(x => x.Notification)
                                  .Include(x => x.Khasra)
                                  .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                   && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                   && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                  .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderBy(a => a.Notification.Name)
                                               .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderBy(a => a.Village.Name)
                                               .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderBy(a => a.Khasra.Name)
                                              .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);


                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderByDescending(a => a.IsActive)
                                              .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);


                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderByDescending(a => a.Notification.Name)
                                               .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderByDescending(a => a.Village.Name)
                                               .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderByDescending(a => a.Khasra.Name)
                                              .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);


                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandus6plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.Name.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderBy(a => a.IsActive)
                                              .GetPaged<Newlandus6plot>(model.PageNumber, model.PageSize);


                        break;

                }
            }
            return data;
        }
        public async Task<List<Newlandus6plot>> GetAllUS6Plot()
        {
            return await _dbContext.Newlandus6plot
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

        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }

    }
}
