﻿using Dto.Search;
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
                                   && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                   && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                  .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderBy(a => a.Notification.NotificationNo)
                                               .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderBy(a => a.Village.Name)
                                               .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize); 
                                               
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderBy(a => a.Khasra.Name)
                                              .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);

                      
                        break;
                   

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderByDescending(a => a.IsActive)
                                              .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);
          

                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderByDescending(a => a.Notification.NotificationNo)
                                               .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                               .Include(x => x.Village)
                                               .Include(x => x.Notification)
                                               .Include(x => x.Khasra)
                                               .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                                && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                                && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                               .OrderByDescending(a => a.Village.Name)
                                               .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderByDescending(a => a.Khasra.Name)
                                              .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);


                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandus4plot
                                              .Include(x => x.Village)
                                              .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                              .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                               && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                              .OrderBy(a => a.IsActive)
                                              .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);


                        break;

                }
            }
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

        public async Task<List<Newlandus4plot>> GetAllUS4PlotList(Newlandus4plotSearchDto model)
        {
            var data = await _dbContext.Newlandus4plot
                                  .Include(x => x.Village)
                                  .Include(x => x.Notification)
                                  .Include(x => x.Khasra)
                                  .Where(x => (string.IsNullOrEmpty(model.locality) || x.Village.Name.Contains(model.locality))
                                   && (string.IsNullOrEmpty(model.notification) || x.Notification.NotificationNo.Contains(model.notification))
                                   && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra))).ToListAsync();
            return data;
        }
        public async Task<List<Newlandnotification>> GetAllNotification()
        {
            List<Newlandnotification> notificationList = await _dbContext.Newlandnotification
                                                          .Where(x => x.IsActive == 1 && x.NotificationTypeId==1 ).ToListAsync();
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
        public async Task<Newlandkhasra> FetchSingleKhasra1Result(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }
        //public async Task<PagedResult<Newlandus4plot>> GetAllFetchNotificationDetails(NewLandNotification4ListSearchDto model)
        //{
        //    var data = await _dbContext.Newlandus4plot
        //                                .Include(x => x.Village)
        //                                 .Include(x => x.Notification)
        //                                      .Include(x => x.Khasra)
        //                                .Where(x => x.NotificationId ==model.NotificationId)
        //                                .OrderByDescending(x => x.Id)
        //                                .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);
        //    return data;
        //}
        public async Task<PagedResult<Newlandus4plot>> GetAllFetchNotificationDetails(NewLandNotification4ListSearchDto model)
        {
            var data = await _dbContext.Newlandus4plot
                                        .Include(x => x.Village)
                                         .Include(x => x.Notification)
                                              .Include(x => x.Khasra)
                                        .Where(x => x.NotificationId == model.NotificationId)
                                        .OrderByDescending(x => x.Id)
                                         .GetPaged<Newlandus4plot>(model.PageNumber, model.PageSize);
            return data;
        }

    }
}
