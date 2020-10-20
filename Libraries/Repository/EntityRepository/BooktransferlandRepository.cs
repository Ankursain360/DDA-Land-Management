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
            return await _dbContext.Booktransferland
                  .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Include(x => x.LandNotification)
                .Where(x => x.IsActive == 1)
                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);
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
      
       
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }


    }
}
