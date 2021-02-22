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
                                       .Include(x => x.LandNotification)
                                     //  .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.LandNotification)
                                      // .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
                                       .OrderBy(a => a.LandNotification.Name)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.LandNotification)
                                     //  .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
                                       .OrderBy(a => a.NotificationDate)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.LandNotification)
                                      // .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
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
                                       .Include(x => x.LandNotification)
                                     //  .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
                                       .OrderByDescending(a => a.LandNotification.Name)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.LandNotification)
                                      // .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
                                       .OrderByDescending(a => a.NotificationDate)
                                       .GetPaged<Ldoland>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Ldoland
                                       .Include(x => x.LandNotification)
                                     //  .Include(x => x.Serialnumber)
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.LandNotification.Name.Contains(model.notification)))
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
            return await _dbContext.Ldoland.Include(x => x.LandNotification)
                //.Include(x => x.Serialnumber)
                .ToListAsync();
        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1).ToListAsync();
            return landNotificationList;
        }


        //public async Task<List<Serialnumber>> GetAllSerialnumber()
        //{
        //    List<Serialnumber> serialnumberList = await _dbContext.Serialnumber.Where(x => x.IsActive == 1).ToListAsync();
        //    return serialnumberList;
        //}
        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Page.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}


    }
}
