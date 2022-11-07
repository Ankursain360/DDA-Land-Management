using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
  public  class NewlandnotificationRepository : GenericRepository<Newlandnotification>, INewlandnotificationRepository
    {
        public NewlandnotificationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> DeleteNewlandnotification(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Newlandnotification.Where(x => x.Id == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Deletefiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Newlandnotificationfilepath.Where(x => x.NewlandNotificationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Newlandnotification> FetchSingleResult(int id)
        {
            return await _dbContext.Newlandnotification
                            .Include(x => x.Newlandnotificationfilepath)
                            .Include(x => x.notificationtypeList)
                              .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Newlandnotification.AnyAsync(t => t.Id != id && t.NotificationNo.ToLower() == name.ToLower());
        }
        public async Task<List<Newlandnotification>> GetAllNewlandNotification()
        {
            return await _dbContext.Newlandnotification.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Newlandnotification>> GetAllNewlandnotificationdetailsList(NewlandnotificationSearchDto model)
        {

            var data = await _dbContext.Newlandnotification
                                 // .Include(x => x.Newlandnotificationfilepath)
                                 //.Include(x => x.NewlandNotificationtype)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderByDescending(s => s.IsActive).ToListAsync();
            return data;
        }

        public async Task<Newlandnotification> NewLandNotificationFile(int Id)
        {
            return await _dbContext.Newlandnotification.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }


        public async Task<List<NewlandNotificationtype>> GetAllNotificationType()
        {
            return await _dbContext.NewlandNotificationtype.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Newlandnotificationfilepath>> GetAllfiledetails(int Id)
        {
            return await _dbContext.Newlandnotificationfilepath.Where(x => x.Id == Id && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<NewlandNotificationtype>> GetNotificationType()
        {
            var notificationtypelist = await _dbContext.NewlandNotificationtype.Where(x => x.IsActive == 1).ToListAsync();
            return notificationtypelist;
        }
        public async Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model)
        {

            var data = await _dbContext.Newlandnotification
                             // .Include(x => x.Newlandnotificationfilepath)
                             //.Include(x => x.NewlandNotificationtype)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name) )
                              .OrderByDescending(s => s.IsActive)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                             // .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                             //    // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderBy(s => s.NotificationType.NotificationType)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                              .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                                   .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                                .OrderByDescending(s => s.IsActive)
                                 .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                              .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                                  .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                               .OrderBy(s => s.NotificationNo)
                                .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize); break;
                    
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                              .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderByDescending(s => s.NotificationType.NotificationType)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                              .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderBy(s => s.IsActive)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                              .Include(x => x.Newlandnotificationfilepath)
                             .Include(x => x.NotificationType)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderByDescending(s => s.NotificationNo)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


        public async Task<bool> SaveNewlandNotification(Newlandnotification newlandnotification)
        {
            _dbContext.Newlandnotification.Add(newlandnotification);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Savefiledetails(Newlandnotificationfilepath newlandnotificationfilepath)
        {
            _dbContext.Newlandnotificationfilepath.Add(newlandnotificationfilepath);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        
    }
}
