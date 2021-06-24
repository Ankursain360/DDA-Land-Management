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
  
    public class LdolandRepository : GenericRepository<Ldoland>, ILdolandRepository
    {
        public LdolandRepository(DataContext dbContext) : base(dbContext)
        {

        }

         public async Task<PagedResult<Ldoland>> GetPagedLdoland(LdolandSearchDto model)
        {
            var data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                   
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                     
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderBy(a => a.OtherLandNotification.NotificationNumber)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                  
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderBy(a => a.NotificationDate)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                      
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderByDescending(a => a.IsActive)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                    
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderByDescending(a => a.OtherLandNotification.NotificationNumber)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                     
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderByDescending(a => a.NotificationDate)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.OtherLandNotification)
                                  
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.OtherLandNotification.NotificationNumber.Contains(model.notification)))
                                       .OrderBy(a => a.IsActive)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }

        public async Task<List<Ldoland>> GetLdoland()
        {
            return await _dbContext.Ldoland.ToListAsync();
        }
        public async Task<List<Ldoland>> GetAllLdoland()
        {
            return await _dbContext.Ldoland.Include(x => x.OtherLandNotification)
              
                .ToListAsync();
        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1).ToListAsync();
            return landNotificationList;
        }

        public async Task<List<Otherlandnotification>> GetAllOtherLandNotification()
        {
            List<Otherlandnotification> List = await _dbContext.Otherlandnotification.Where(x => (x.IsActive == 1) && (x.LandType=="LDO")).ToListAsync();
            return List;
        }


    }
}
