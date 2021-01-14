using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

  public class WatchandwardRepository : GenericRepository<Watchandward>, IWatchandwardRepository
    {
        public WatchandwardRepository(DataContext dbContext) : base(dbContext)
        {

        }
      

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model)
        {
            var data = await _dbContext.Watchandward
                .Include(x => x.PrimaryListNoNavigation)
                .Include(x => x.PrimaryListNoNavigation.Locality)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                            && (x.IsActive == 1))
                .GetPaged<Watchandward>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                   
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.PrimaryListNoNavigation.KhasraNo).ToList();
                        break;
                    case ("PRIMARYLISTNO"):
                        data.Results = data.Results.OrderBy(x => x.PrimaryListNo).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.PrimaryListNoNavigation.KhasraNo).ToList();
                        break;
                    case ("PRIMARYLISTNO"):
                        data.Results = data.Results.OrderByDescending(x => x.PrimaryListNo).ToList();
                        break;

                }
            }
            return data;
        }



        public async Task<List<Watchandward>> GetWatchandward()
        {
            return await _dbContext.Watchandward.ToListAsync();
        }
        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _dbContext.Watchandward
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .ToListAsync();
        }
        public async Task<Watchandward> FetchSingleResult(int id)
        {
            return await _dbContext.Watchandward
                .Include(x => x.Watchandwardphotofiledetails)
                .Include(x => x.Watchandwardreportfiledetails).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        //public async Task<List<Village>> GetAllVillage()
        //{
        //    List<Village> villagelist = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
        //    return villagelist;
        //}
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<PagedResult<Watchandward>> GetWatchandwardReportData(WatchandwardSearchDto watchandwardSearchDto)
        {
            var data = await _dbContext.Watchandward
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Where(x => (x.LocalityId == (watchandwardSearchDto.localityId == 0 ? x.LocalityId : watchandwardSearchDto.localityId))
               && x.Date >= watchandwardSearchDto.fromDate
               && x.Date <= watchandwardSearchDto.toDate)
                .OrderByDescending(x => x.Id).GetPaged(watchandwardSearchDto.PageNumber, watchandwardSearchDto.PageSize);

            return data;
        }


        //**************multiple files methods********************* added by ishu

        public async Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails watchandwardphotofiledetails)
        {
            _dbContext.Watchandwardphotofiledetails.Add(watchandwardphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails watchandwardreportfiledetails)
        {
            _dbContext.Watchandwardreportfiledetails.Add(watchandwardreportfiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        
        public async Task<Watchandwardphotofiledetails> GetWatchandwardphotofiledetails(int watchandwardId)
        {
            return await _dbContext.Watchandwardphotofiledetails.Where(x => x.Id == watchandwardId && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Watchandwardreportfiledetails> GetWatchandwardreportfiledetails(int watchandwardId)
        {
            return await _dbContext.Watchandwardreportfiledetails.Where(x => x.Id == watchandwardId && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteWatchandwardphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Watchandwardphotofiledetails.Where(x => x.WatchAndWardId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> DeleteWatchandwardreportfiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Watchandwardreportfiledetails.Where(x => x.WatchAndWardId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Propertyregistration>> GetAllPrimaryList()
        {
            return await _dbContext.Propertyregistration.Where(x => x.IsActive == 1 && x.IsDeleted == 1 &&  x.IsValidate == 1 && x.IsDisposed != 0).ToListAsync();
        }

        public async Task<Propertyregistration> FetchSingleResultOnPrimaryList(int propertyId)
        {
            return await _dbContext.Propertyregistration.Where(x => x.Id == propertyId).FirstOrDefaultAsync();
        }

    }
}
