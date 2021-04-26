

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
    public class NewlandnotificationdetailsRepository : GenericRepository<Newlandnotificationdetails>, INewlandnotificationdetailsRepository
    {
        public NewlandnotificationdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandnotificationdetails>> GetPagedNotifications(NewlandnotificationdetailsSearchDto model)
        {
            var data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                       .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                         .Include(x => x.NotificationType)
                                         .Include(x => x.Village)
                                         .Include(x => x.Khasra)
                                         .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                         && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                         .OrderBy(a => a.NotificationType.NotificationType)
                                         .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);

                                           

                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                         .Include(x => x.NotificationType)
                                         .Include(x => x.Village)
                                         .Include(x => x.Khasra)
                                         .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                         && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                         .OrderBy(a => a.NotificationNo)
                                         .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                        .OrderBy(a => a.Village.Name)
                                        .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                        .OrderBy(a => a.Khasra.Name)
                                        .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);
                                             

                        break;
                    case ("STATUS"):
                        data = null;
                         data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                        .OrderByDescending(x => x.IsActive)
                                        .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);
                            
                          
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                         .Include(x => x.NotificationType)
                                         .Include(x => x.Village)
                                         .Include(x => x.Khasra)
                                         .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                         && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                         .OrderByDescending(a => a.NotificationType.NotificationType)
                                         .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);



                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                         .Include(x => x.NotificationType)
                                         .Include(x => x.Village)
                                         .Include(x => x.Khasra)
                                         .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                         && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                         .OrderByDescending(a => a.NotificationNo)
                                         .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                        .OrderByDescending(a => a.Village.Name)
                                        .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                        .Include(x => x.NotificationType)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                        && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                        && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                        && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                        .OrderByDescending(a => a.Khasra.Name)
                                        .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandnotificationdetails
                                       .Include(x => x.NotificationType)
                                       .Include(x => x.Village)
                                       .Include(x => x.Khasra)
                                       .Where(x => (string.IsNullOrEmpty(model.type) || x.NotificationType.NotificationType.Contains(model.type))
                                       && (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                       && (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))
                                       && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                       .OrderBy(x => x.IsActive)
                                       .GetPaged<Newlandnotificationdetails>(model.PageNumber, model.PageSize);


                        break;
                }
            }
            return data;
           
        }
       
        public async Task<List<Newlandnotificationdetails>> GetAllNotifications()
        {
            return await _dbContext.Newlandnotificationdetails
                                     .Include(x => x.NotificationType)
                                     .Include(x => x.Village)
                                     .Include(x => x.Khasra)
                                     .ToListAsync();
        }
        public async Task<List<NewlandNotificationtype>> GetAllNotificationType()
        {
            List<NewlandNotificationtype> notificationtypeList = await _dbContext.NewlandNotificationtype.Where(x => x.IsActive == 1).ToListAsync();
            return notificationtypeList;
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
