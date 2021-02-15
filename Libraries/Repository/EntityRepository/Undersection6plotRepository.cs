using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.EntityRepository
{
   public class Undersection6plotRepository : GenericRepository<Undersection6plot>, IUndersection6plotRepository
    {
        public Undersection6plotRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<List<Undersection6>> GetAllNotificationNo()
        {
            List<Undersection6> notificationList = await _dbContext.Undersection6.Where(x => x.IsActive == 1).ToListAsync();
            return notificationList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }



        public async Task<List<Undersection6plot>> GetAllUndersection6Plot()
        {
            return await _dbContext.Undersection6plot.Include(x => x.Undersection6).Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Undersection6plot>> GetPagedNoUndersection6plot(NotificationUndersection6plotDto model)
        {
            var data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                   && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               ).




                GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);









            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                   && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               )
                                .OrderBy(s => s.Undersection6.Number)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
                   .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                      && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderBy(s => s.Village.Name)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
               .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                  && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

              )
                           .OrderByDescending(s => s.IsActive)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                   && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               )
                                .OrderByDescending(s => s.Undersection6.Number)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
                   .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                      && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderByDescending(s => s.Village.Name)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection6plot.Include(x => x.Undersection6)
               .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification6) || x.Undersection6.Number.Contains(model.numbernotification6))
                  && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

              )
                           .OrderBy(s => s.IsActive)
                                .GetPaged<Undersection6plot>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }





        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }




    }
}
