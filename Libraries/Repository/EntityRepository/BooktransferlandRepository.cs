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
   
      public class BooktransferlandRepository : GenericRepository<Booktransferland>, IBooktransferlandRepository
    {
        public BooktransferlandRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Booktransferland>> GetPagedBooktransferland(BooktransferlandSearchDto model)
        {
            //return await _dbContext.Booktransferland
            //      .Include(x => x.Locality)
            //    .Include(x => x.Khasra)
            //    .Include(x => x.LandNotification)
            //   // .Where(x => x.IsActive == 1)
            //    .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Booktransferland
                 .Include(x => x.Locality)
                
                .Include(x => x.Khasra)
                .Include(x => x.LandNotification)
                // .Where(x => x.IsActive == 1)
                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATIONNO"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderBy(s => s.LandNotification.Name)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("PART"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderBy(s => s.Part)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("NOTIFICATIONDATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderBy(s => s.NotificationDate)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("POSESSIONDATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderBy(s => s.DateofPossession)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    

                       
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderByDescending(s => s.IsActive == 0)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATIONNO"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderByDescending(s => s.LandNotification.Name)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("PART"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderByDescending(s => s.Part)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("NOTIFICATIONDATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderByDescending(s => s.NotificationDate)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("POSESSIONDATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderByDescending(s => s.DateofPossession)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                   
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                   .Include(x => x.Locality)
                                   .Include(x => x.Khasra)
                        .Include(x => x.LandNotification)
                 .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                            .OrderBy(s => s.IsActive == 0)
                        .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }
        public async Task<List<Booktransferland>> GetBooktransferland()
        {
            return await _dbContext.Booktransferland.ToListAsync();
        }
        public async Task<List<Booktransferland>> GetAllBooktransferland()
        {
            return await _dbContext.Booktransferland
                .Include(x => x.LandNotification)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Where(x => x.IsActive == 1)
                .ToListAsync();


        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1).ToListAsync();
            return landNotificationList;
        }
        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllLocality()
        {
            List<Acquiredlandvillage> localityList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }


    }
}
