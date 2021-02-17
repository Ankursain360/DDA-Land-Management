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
    public class Undersection4plotRepository :  GenericRepository<Undersection4plot>, IUnderSection4PlotRepository
    {
        public Undersection4plotRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Undersection4>> GetAllNotificationNo()
        {
            List<Undersection4> notificationList = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
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





        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }












        public async Task<List<Undersection4plot>> GetAllUndersection4Plot()
        {
            return await _dbContext.Undersection4plot.Include(x => x.UnderSection4).Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Undersection4plot>> GetPagedNoUndersection4plot(NotificationUndersection4plotDto model)
        {
            var data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                .Include(x=>x.Village).Include(x=>x.Khasra) .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               ).




                GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);









            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                   && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               )
                                .OrderBy(s => s.UnderSection4.Number)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                   .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                      && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderBy(s => s.Village.Name)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
               .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                  && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

              )
                           .OrderByDescending(s => s.IsActive)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                   && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

               )
                                .OrderByDescending(s => s.UnderSection4.Number)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
                   .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                      && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

                  )
                                    .OrderByDescending(s => s.Village.Name)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection4plot.Include(x => x.UnderSection4)
               .Include(x => x.Village).Include(x => x.Khasra).Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.UnderSection4.Number.Contains(model.numbernotification4))
                  && (string.IsNullOrEmpty(model.villageid) || x.Village.Name.Contains(model.villageid))

              )
                           .OrderBy(s => s.IsActive)
                                .GetPaged<Undersection4plot>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }



    }
}
