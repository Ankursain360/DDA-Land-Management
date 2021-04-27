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
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
  
    public class Undersection22plotdetailsRepository : GenericRepository<Undersection22plotdetails>, IUndersection22plotdetailsRepository
    {
        public Undersection22plotdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Undersection22plotdetails>> GetAllUS22PlotDetails()
        {
            return await _dbContext.Undersection22plotdetails
                                   .Include(x => x.UnderSection22)
                                   .Include(x => x.Khasra)
                                   .Include(x => x.Acquiredlandvillage)
                                   .Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Acquiredlandvillage>> GetAllAcquiredlandvillage()
        {
            List<Acquiredlandvillage> acqvillageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return acqvillageList;
        }

        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }
        public async Task<List<Undersection4>> GetAllUndersection4()
        {
            List<Undersection4> us4List = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return us4List;
        }
        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            List<Undersection6> us6List = await _dbContext.Undersection6.Where(x => x.IsActive == 1).ToListAsync();
            return us6List;
        }
        public async Task<List<Undersection17>> GetAllUndersection17()
        {
            List<Undersection17> us17List = await _dbContext.Undersection17.Where(x => x.IsActive == 1).ToListAsync();
            return us17List;
        }
        public async Task<List<Undersection22>> GetAllUndersection22()
        {
            List<Undersection22> us22List = await _dbContext.Undersection22.Where(x => x.IsActive == 1).ToListAsync();
            return us22List;
        }

        public async Task<PagedResult<Undersection22plotdetails>> GetPagedUndersection22plotdetails(Undersection22plotdetailsSearchDto model)
        {
            var data = await _dbContext.Undersection22plotdetails
                              .Include(x => x.UnderSection22)
                              .Include(x => x.Acquiredlandvillage)
                              .Include(x => x.Khasra)
                             .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                             && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                             && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                            .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                     .Include(x => x.UnderSection22)
                                      .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderBy(a => a.UnderSection22.NotificationNo)
                                      .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);
  
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                      .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderBy(a => a.Acquiredlandvillage.Name)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);
  
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                       .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderBy(a => a.Khasra.Name)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                      .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderByDescending(a => a.IsActive)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                     .Include(x => x.UnderSection22)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                     && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.UnderSection22.NotificationNo)
                                    .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);

                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                      .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                      .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderByDescending(a => a.Khasra.Name)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection22plotdetails
                                      .Include(x => x.UnderSection22)
                                       .Include(x => x.Acquiredlandvillage)
                                      .Include(x => x.Khasra)
                                      .Where(x => (string.IsNullOrEmpty(model.usno) || x.UnderSection22.NotificationNo.Contains(model.usno))
                                      && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                                      && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                     .OrderBy(a => a.IsActive)
                                     .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;
        }



        public async Task<List<Unotification22detailsListDto>> GetPagednotification22detailsList(Unotification22detailsSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("BindUnderSection22Details")
                                            .WithSqlParams(("P_UnSec22Id", model.notification22))



                                            .ExecuteStoredProcedureAsync<Unotification22detailsListDto>();

                return (List<Unotification22detailsListDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PagedResult<Undersection22plotdetails>> GetAllNotificationList(NotificationList22SearchDto model)
        {
            var data = await _dbContext.Undersection22plotdetails
                                        .Include(x => x.Acquiredlandvillage)
                                         .Include(x => x.UnderSection22)
                                              .Include(x => x.Khasra)
                                        .Where(x => x.UnderSection22Id == model.NotificationId)
                                        .OrderByDescending(x => x.Id)
                                         .GetPaged<Undersection22plotdetails>(model.PageNumber, model.PageSize);
            return data;
        }

    }
}
