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
   public class NewLandNotificationTypeRepository : GenericRepository<NewlandNotificationtype>,INewLandNotificationTypeRepository
    {
        public NewLandNotificationTypeRepository(DataContext dbContext) : base(dbContext)
        {
            

        }

        public async Task<PagedResult<NewlandNotificationtype>> GetPagedZone(NewLandNotificationTypeSearchDto model)
        {
            var data = await _dbContext.NewlandNotificationtype
                           .Where(x => (string.IsNullOrEmpty(model.Name) || x.NotificationType.Contains(model.Name)))
                           .GetPaged<NewlandNotificationtype>(model.PageNumber, model.PageSize); ;
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.NewlandNotificationtype
                           .Where(x => (string.IsNullOrEmpty(model.Name) || x.NotificationType.Contains(model.Name)))
                           .OrderBy(s => s.NotificationType)
                           .GetPaged<NewlandNotificationtype>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.NewlandNotificationtype
                           .Where(x => (string.IsNullOrEmpty(model.Name) || x.NotificationType.Contains(model.Name)))
                           .OrderByDescending(s => s.IsActive)
                           .GetPaged<NewlandNotificationtype>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.NewlandNotificationtype
                           .Where(x => (string.IsNullOrEmpty(model.Name) || x.NotificationType.Contains(model.Name)))
                           .OrderByDescending(s => s.NotificationType)
                           .GetPaged<NewlandNotificationtype>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.NewlandNotificationtype
                           .Where(x => (string.IsNullOrEmpty(model.Name) || x.NotificationType.Contains(model.Name)))
                           .OrderBy(s => s.IsActive)
                           .GetPaged<NewlandNotificationtype>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


    }
}
