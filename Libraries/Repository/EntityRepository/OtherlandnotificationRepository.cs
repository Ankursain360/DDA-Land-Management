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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
   public class OtherlandnotificationRepository : GenericRepository<Otherlandnotification>, IOtherlandnotificationRepository
    {
        public OtherlandnotificationRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Otherlandnotification>> GetOtherlandnotification()
        {
            return await _dbContext.Otherlandnotification.ToListAsync();
        }


        public async Task<PagedResult<Otherlandnotification>> GetPagedOtherlandnotification(OtherlandnotificationSearchDto model)
        {
            var data = await _dbContext.Otherlandnotification

                                       .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                      && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                        )
                                        .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LANDTYPE"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                             .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                      && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                        )
                             .OrderBy(x => x.LandType)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATIONNUMBER"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                            .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                     && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                       )
                            .OrderBy(x => x.NotificationNumber)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                              .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                     && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                       )
                             .OrderByDescending(x => x.IsActive)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LANDTYPE"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                              .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                     && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                       )
                             .OrderByDescending(x => x.LandType)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATIONNUMBER"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                             .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                    && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                      )
                            .OrderByDescending(x => x.NotificationNumber)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                  

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Otherlandnotification

                               .Where(x => (string.IsNullOrEmpty(model.landtype) || x.LandType.Contains(model.landtype))
                                    && (string.IsNullOrEmpty(model.notificationnumber) || x.NotificationNumber.Contains(model.notificationnumber))
                                      )
                             .OrderBy(x => x.IsActive)
                            .GetPaged<Otherlandnotification>(model.PageNumber, model.PageSize);
                        break;
                }
            }




            return data;
        }


    }
}
