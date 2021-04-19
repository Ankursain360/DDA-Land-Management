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

using Repository.Common;


namespace Libraries.Repository.EntityRepository
{
    public class Undersection17plotdetailRepository : GenericRepository<Undersection17plotdetail>, IUndersection17plotdetailRepository
    {
        public Undersection17plotdetailRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model)
        {
           
            var data = await _dbContext.Undersection17plotdetail
                       .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                        .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Khasra.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .OrderBy(a => a.UnderSection17.Number)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                   
               
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Khasra.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .OrderByDescending(a => a.UnderSection17.Number)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;

        }


        
        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
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
        public async Task<List<Undersection17>> GetAllUndersection17List()
        {
            List<Undersection17> undersection17List = await _dbContext.Undersection17.ToListAsync();
            return undersection17List;
        }




        public async Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail()
        {
            return await _dbContext.Undersection17plotdetail.Include(x => x.UnderSection17).Include(x => x.Khasra).Include(x => x.Acquiredlandvillage).OrderByDescending(x => x.Id).ToListAsync();
        }

       


        public async Task<List<Unotification17detailsListDto>> GetPagednotification17detailsList(Unotification17detailsSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("BindUnderSection17Details")
                                            .WithSqlParams(("P_UnSec17Id", model.notification17))



                                            .ExecuteStoredProcedureAsync<Unotification17detailsListDto>();

                return (List<Unotification17detailsListDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PagedResult<Undersection17plotdetail>> GetAllNotificationList(NotificationList17SearchDto model)
        {
            var data = await _dbContext.Undersection17plotdetail
                                        .Include(x => x.Acquiredlandvillage)
                                         .Include(x => x.UnderSection17)
                                              .Include(x => x.Khasra)
                                        .Where(x => x.UnderSection17Id == model.NotificationId)
                                        .OrderByDescending(x => x.Id)
                                         .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
            return data;
        }


    }
}

