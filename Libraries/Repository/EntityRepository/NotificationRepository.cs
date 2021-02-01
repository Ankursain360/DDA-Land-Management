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

namespace Libraries.Repository.EntityRepository
{
    public class NotificationRepository : GenericRepository<LandNotification>, INotificationRepository
    {

        public NotificationRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.LandNotification.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<LandNotification>> GetPagedZone(NotificationSearchDto model)
        {
            var data = await _dbContext.LandNotification
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .GetPaged<LandNotification>(model.PageNumber, model.PageSize); ;
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.LandNotification
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(s => s.Name)
                           .GetPaged<LandNotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.LandNotification
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(s => s.IsActive)
                           .GetPaged<LandNotification>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.LandNotification
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(s => s.Name)
                           .GetPaged<LandNotification>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.LandNotification
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(s => s.IsActive)
                           .GetPaged<LandNotification>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
    }


}
