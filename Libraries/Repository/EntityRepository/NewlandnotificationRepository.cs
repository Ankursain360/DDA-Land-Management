using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;

namespace Libraries.Repository.EntityRepository
{
  public  class NewlandnotificationRepository : GenericRepository<Newlandnotification>, INewlandnotificationRepository
    {
        public NewlandnotificationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Newlandnotification.AnyAsync(t => t.Id != id && t.NotificationNo.ToLower() == name.ToLower());
        }
        public async Task<List<Newlandnotification>> GetNewlandnotificationdetails()
        {
            return await _dbContext.Newlandnotification.Where(x => x.IsActive == 1).ToListAsync();
            //return await _dbContext.casenature.OrderByDescending(s => s.IsActive).ToListAsync();
        }
        public async Task<List<NewlandNotificationtype>> GetNotificationType()
        {
            var notificationtypelist = await _dbContext.NewlandNotificationtype.Where(x => x.IsActive == 1).ToListAsync();
            return notificationtypelist;
        }
        public async Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model)
        {

            var data = await _dbContext.Newlandnotification
                              // .Include(x => x.Noti)
                              //.Include(x=>x.Us17)
                              //.Include(x=>x.Us4)
                              //.Include(x=>x.Us6)
                             // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name) )
                              .OrderByDescending(s => s.IsActive)
                             .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                 // .Include(x => x.Noti)
                                 //.Include(x=>x.Us17)
                                 //.Include(x=>x.Us4)
                                 //.Include(x=>x.Us6)
                                 // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderBy(s => s.NotificationNo)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                   // .Include(x => x.Noti)
                                   //.Include(x=>x.Us17)
                                   //.Include(x=>x.Us4)
                                   //.Include(x=>x.Us6)
                                   // .Include(x => x.Proposal)
                                   .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                                .OrderByDescending(s => s.IsActive)
                                 .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                  // .Include(x => x.Noti)
                                  //.Include(x=>x.Us17)
                                  //.Include(x=>x.Us4)
                                  //.Include(x=>x.Us6)
                                  // .Include(x => x.Proposal)
                                  .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                               .OrderBy(s => s.Date)
                                .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize); break;
                    
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                 // .Include(x => x.Noti)
                                 //.Include(x=>x.Us17)
                                 //.Include(x=>x.Us4)
                                 //.Include(x=>x.Us6)
                                 // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderByDescending(s => s.NotificationNo)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                 // .Include(x => x.Noti)
                                 //.Include(x=>x.Us17)
                                 //.Include(x=>x.Us4)
                                 //.Include(x=>x.Us6)
                                 // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderBy(s => s.IsActive)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandnotification
                                 // .Include(x => x.Noti)
                                 //.Include(x=>x.Us17)
                                 //.Include(x=>x.Us4)
                                 //.Include(x=>x.Us6)
                                 // .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.NotificationNo.Contains(model.name))
                              .OrderByDescending(s => s.Date)
                               .GetPaged<Newlandnotification>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }

    }
}
